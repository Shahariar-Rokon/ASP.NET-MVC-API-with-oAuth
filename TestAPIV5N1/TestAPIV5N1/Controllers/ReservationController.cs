using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TestAPIV5N1.Models;

namespace TestAPIV5N1.Controllers
{
    public class ReservationController : ApiController
    {
        private AppDbContext db = new AppDbContext();
        [Authorize(Roles ="Admin")]
        public System.Object GetReservation()
        {
            var res = db.Reservations.ToList();
            return res;
        }
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetReservationById(int id)
        {
            var res = (from r in db.Reservations
                       where r.ReservationId == id
                       select new
                       {
                           r.ReservationId,r.ReservationNo,
                           r.ReservationDate,r.GuestName,r.ImageUrl,
                           r.IsPaid
                       });
            var rs = (from r in db.ReservedItems
                      join s in db.Services on r.ServiceId equals s.ServiceId
                      where r.ReservationId == id
                      select new
                      {
                          r.ServiceId,r.ReservationId,s.ServiceName,s.Price
                      });
            return Ok(new {res,rs});
        }
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(int id)
        {
            var res = db.Reservations.Find(id);
            db.Reservations.Remove(res);
            var resit = db.ReservedItems.Where(x=>x.ReservationId == id);
            foreach(var item in resit)
            {
                db.ReservedItems.Remove(item);
            }
            db.SaveChanges();
            return Ok("Deleted");
        }
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PostReservation(ReserveRequest request)
        {
            if(request.Reservation == null)
            {
                return BadRequest();
            }
            Reservation res = request.Reservation;
            byte[] imageFile = request.ImageFile;
            if(imageFile!=null&& imageFile.Length>0)
            {
                var filename = Guid.NewGuid().ToString() + ".jpg";
                var filepath = Path.Combine("~/Img/", filename);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(filepath), imageFile);
                res.ImageUrl = filepath;
            }
            Reservation Reservation = new Reservation
            {
                ReservationNo = res.ReservationNo,
                ReservationDate = res.ReservationDate,
                GuestName = res.GuestName,
                ImageUrl = res.ImageUrl,
                IsPaid = res.IsPaid
            };
            db.Reservations.Add(Reservation); 
            db.SaveChanges();
            var resobj = db.Reservations.FirstOrDefault(x=>x.ReservationNo==res.ReservationNo);
            if (resobj != null && res.ReservedItems != null)
            {
                foreach(var item in res.ReservedItems)
                {
                    ReservedItem ri = new ReservedItem
                    {
                        ReservationId = resobj.ReservationId,
                        ServiceId = item.ServiceId
                    };
                    db.ReservedItems.Add(ri);
                }
            }
            db.SaveChanges();
            return Ok("Successfully posted");
        }
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutReservation(int id,ReserveRequest request)
        {
            Reservation reserve = db.Reservations.FirstOrDefault(x => x.ReservationId == id);
            if (id == null || request.Reservation.ReservationId == null || request == null)
            {
                return BadRequest();
            }
            Reservation res = request.Reservation;
            byte[] imageFile = request.ImageFile;
            if (imageFile != null && imageFile.Length > 0)
            {
                var filename = Guid.NewGuid().ToString() + ".jpg";
                var filepath = Path.Combine("~/Img/", filename);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(filepath), imageFile);
                res.ImageUrl = filepath;
            }
            Reservation Reservation = new Reservation
            {
                ReservationNo = res.ReservationNo,
                ReservationDate = res.ReservationDate,
                GuestName = res.GuestName,
                ImageUrl = res.ImageUrl,
                IsPaid = res.IsPaid
            };
            reserve.ReservationNo = res.ReservationNo;
            reserve.ReservationDate = res.ReservationDate;
            reserve.GuestName = res.GuestName;
            reserve.ImageUrl = res.ImageUrl;
            reserve.IsPaid = res.IsPaid;
            var exRest = db.ReservedItems.Where(x=>x.ReservationId==reserve.ReservationId).ToList();
            db.ReservedItems.RemoveRange(exRest);
            if (res.ReservedItems != null)
            {
                foreach(var item in res.ReservedItems)
                {
                    ReservedItem rt = new ReservedItem
                    {
                        ReservationId = reserve.ReservationId,
                        ServiceId = item.ServiceId,
                    };
                    db.ReservedItems.Add(rt);
                }
            }
            db.SaveChanges();
            return Ok("Successfully Updated");
        }
    }
}
