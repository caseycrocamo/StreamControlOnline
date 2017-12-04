using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using StreamControl.Data;
using StreamControl.Models;
using System.Web.Http.Cors;
using System.Web;

namespace StreamControl.Controllers
{
    //[RequireHttps]
    [Authorize]
    public class OverlayController : ApiController
    {
        private OverlayContext db = new OverlayContext();

        // GET: api/Overlays?username
        [HttpGet]
        [ResponseType(typeof(Overlay[]))]
        public async Task<IHttpActionResult> Getoverlays(string username)
        {
            Overlay[] query = await db.Overlays.Where(u => u.OwnerID == username).ToArrayAsync<Overlay>();
            return Ok(query);
        }

        // GET: api/Overlays/5
        [HttpGet]
        [ResponseType(typeof(Overlay))]
        public async Task<IHttpActionResult> Getoverlay(int id)
        {
            Overlay overlay = await db.Overlays.FindAsync(id);
            if (overlay == null)
            {
                return NotFound();
            }

            return Ok(overlay);
        }

        // PUT: api/Overlays/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putoverlay(int id, Overlay overlay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != overlay.OverlayID)
            {
                return BadRequest();
            }
         
            db.Entry(overlay).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!overlayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await UpdateElements(overlay.Elements);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Overlays/Element?id
        [HttpPut]
        [Route("api/Overlays/Element")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutElement(int id, Element Element)
        {
            Overlay overlay = await db.Overlays.FirstOrDefaultAsync(i => i.OverlayID == id);
            overlay.Elements.Add(Element);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!overlayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// PUT: api/Overlays/Player
        //[HttpPut]
        //[Route("api/Overlays/Player")]
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutPlayer(int id, Player player)
        //{
        //    Overlay overlay = await db.Overlays.FirstOrDefaultAsync(i => i.overlayID == id);
        //    overlay.Players.Add(player);
        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!overlayExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // PUT: api/overlays/Style
        [HttpPut]
        [Route("api/overlays/Style")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStyle(int id, Style style)
        {
            Style _style = await db.Styles.FirstOrDefaultAsync(i => i.StyleID == id);
            _style = style;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/overlays
        [ResponseType(typeof(Overlay))]
        public async Task<IHttpActionResult> Postoverlay(Overlay overlay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Overlays.Add(overlay);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = overlay.OverlayID }, overlay);
        }

        // DELETE: api/overlays/5
        [ResponseType(typeof(Overlay))]
        public async Task<IHttpActionResult> Deleteoverlay(int id)
        {
            Overlay overlay = await db.Overlays.FindAsync(id);
            if (overlay == null)
            {
                return NotFound();
            }

            db.Overlays.Remove(overlay);
            await db.SaveChangesAsync();

            return Ok(overlay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool overlayExists(int id)
        {
            return db.Overlays.Count(e => e.OverlayID == id) > 0;
        }

        private async Task UpdateElements(ICollection<Element> Elements)
        {
            foreach (Element Element in Elements)
            {
                db.Entry(Element).State = EntityState.Modified;
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


    }
}