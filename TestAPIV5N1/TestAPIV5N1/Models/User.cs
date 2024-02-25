using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestAPIV5N1.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string ReservationNo { get; set; }
        public DateTime ReservationDate { get; set; }
        public string GuestName { get; set; }
        public bool IsPaid { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<ReservedItem> ReservedItems { get; set; }

    }
    public class ReservedItem
    {
        public int ReservedItemId { get; set; }
        public int ReservationId { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }
    [NotMapped]
    public class ReserveRequest
    {
        public Reservation Reservation { get; set; }
        public byte[] ImageFile { get; set; }
        public string ImageFileName { get; set; }
    }
}