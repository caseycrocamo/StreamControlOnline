﻿using System;
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
    public class ScoreboardsController : ApiController
    {
        private ScoreBoardContext db = new ScoreBoardContext();

        // GET: api/Scoreboards?username
        [HttpGet]
        [ResponseType(typeof(Scoreboard[]))]
        public async Task<IHttpActionResult> GetScoreboards(string username)
        {
            Scoreboard[] query = await db.Scoreboards.Where(u => u.OwnerID == username).ToArrayAsync<Scoreboard>();
            return Ok(query);
        }

        // GET: api/Scoreboards/5
        [HttpGet]
        [ResponseType(typeof(Scoreboard))]
        public async Task<IHttpActionResult> GetScoreboard(int id)
        {
            Scoreboard scoreboard = await db.Scoreboards.FindAsync(id);
            if (scoreboard == null)
            {
                return NotFound();
            }

            return Ok(scoreboard);
        }

        // PUT: api/Scoreboards/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutScoreboard(int id, Scoreboard scoreboard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != scoreboard.ScoreboardID)
            {
                return BadRequest();
            }
         
            db.Entry(scoreboard).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreboardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await updateTextElements(scoreboard.TextElements);
            await UpdatePlayerElements(scoreboard.PlayerElements);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Scoreboards/TextElement?id
        [HttpPut]
        [Route("api/Scoreboards/TextElement")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTextElement(int id, TextElement textElement)
        {
            Scoreboard scoreboard = await db.Scoreboards.FirstOrDefaultAsync(i => i.ScoreboardID == id);
            scoreboard.TextElements.Add(textElement);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreboardExists(id))
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

        // PUT: api/Scoreboards/PlayerElement
        [HttpPut]
        [Route("api/Scoreboards/PlayerElement")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayerElement(int id, PlayerElement playerElement)
        {
            Scoreboard scoreboard = await db.Scoreboards.FirstOrDefaultAsync(i => i.ScoreboardID == id);
            scoreboard.PlayerElements.Add(playerElement);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreboardExists(id))
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

        // PUT: api/Scoreboards/Style
        [HttpPut]
        [Route("api/Scoreboards/Style")]
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

        // POST: api/Scoreboards
        [ResponseType(typeof(Scoreboard))]
        public async Task<IHttpActionResult> PostScoreboard(Scoreboard scoreboard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Scoreboards.Add(scoreboard);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = scoreboard.ScoreboardID }, scoreboard);
        }

        // DELETE: api/Scoreboards/5
        [ResponseType(typeof(Scoreboard))]
        public async Task<IHttpActionResult> DeleteScoreboard(int id)
        {
            Scoreboard scoreboard = await db.Scoreboards.FindAsync(id);
            if (scoreboard == null)
            {
                return NotFound();
            }

            db.Scoreboards.Remove(scoreboard);
            await db.SaveChangesAsync();

            return Ok(scoreboard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScoreboardExists(int id)
        {
            return db.Scoreboards.Count(e => e.ScoreboardID == id) > 0;
        }

        private async Task updateTextElements(ICollection<TextElement> textElements)
        {
            foreach (TextElement textElement in textElements)
            {
                db.Entry(textElement).State = EntityState.Modified;
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

        private async Task UpdatePlayerElements(ICollection<PlayerElement> playerElements)
        {
            foreach (PlayerElement playerElement in playerElements)
            {
                db.Entry(playerElement).State = EntityState.Modified;
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