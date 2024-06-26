using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalFinanceWebApp.Models.Entities;

namespace PersonalFinanceWebApp.Models.Dtos
{
    public class CreateTransferDto
    {

        public required Guid SenderAccountID { get; set; }
        public required Guid ReceiverAccountID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransferDate { get; set; }
        public string? Description { get; set; }

        // aynı sekilde burda da dbden accountları çekeceksin
        // transferi create ederken o accountları vereceksin
        // bu idler işte parayı gönderen ve alan accountlar bunları 
        // dtodan geldi ya bu idler, bakacaksın orada db de var mı böyle accountlar
        // yoksa hata vereceksin
        // dtoplari mi ım drekt swaggerdan bakalımgenel

    }
}