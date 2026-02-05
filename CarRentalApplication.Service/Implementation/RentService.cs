using CarRentalApplication.Domain.DomainModels;
using CarRentalApplication.Repository;
using CarRentalApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarRentalApplication.Service.Implementation
{
    public class RentService : IRentService
    {
        private readonly IRepository<Rent> _rentRepository;

        public RentService(IRepository<Rent> rentRepository)
        {
            _rentRepository = rentRepository;
        }

        public IEnumerable<Rent> GetByUserId(string userId)
        {
            return _rentRepository.GetAll(selector: x => x,
                predicate: x => x.OwnerId == userId,
                include: x => x.Include(r => r.CarsInRent).ThenInclude(c => c.Car));
        }

        public Rent? GetById(Guid id)
        {
            return _rentRepository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(r => r.CarsInRent).ThenInclude(c => c.Car));
        }

        public bool FinishRent(Guid rentId, string userId)
        {
            var rent = _rentRepository.Get(selector: x => x,
                predicate: x => x.Id == rentId && x.OwnerId == userId,
                include: x => x.Include(r => r.CarsInRent).ThenInclude(c => c.Car));

            if (rent == null) return false;
            if (rent.IsFinished) return false;

            rent.IsFinished = true;
            _rentRepository.Update(rent);
            return true;
        }

        public byte[] GenerateInvoice(Guid rentId)
        {
            var rent = GetById(rentId);
            if (rent == null) return Array.Empty<byte>();
            if (!rent.IsFinished) return Array.Empty<byte>();

            using var ms = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var fontTitle = new XFont("Verdana", 16);
            var font = new XFont("Verdana", 12);

            int y = 40;
            gfx.DrawString("Invoice", fontTitle, XBrushes.Black, new XPoint(40, y));
            y += 40;

            gfx.DrawString($"Rent Id: {rent.Id}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Owner: {rent.Owner?.UserName ?? rent.OwnerId}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;

            double total = 0.0;
            if (rent.CarsInRent != null)
            {
                foreach (var item in rent.CarsInRent)
                {
                    var car = item.Car;
                    var line = $"{car?.Make} {car?.Model} - {item.Days} days x {car?.PricePerDay:C} = {(item.Days * (car?.PricePerDay ?? 0)):C}";
                    gfx.DrawString(line, font, XBrushes.Black, new XPoint(40, y));
                    y += 20;
                    total += item.Days * (car?.PricePerDay ?? 0);
                }
            }

            y += 10;
            gfx.DrawString($"Total: {total:C}", font, XBrushes.Black, new XPoint(40, y));

            document.Save(ms);
            return ms.ToArray();
        }
    }
}
