using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EduardoPrimavera.Models;
using System.Security.Cryptography;
using System.Collections;
using System.Text;

namespace EduardoPrimavera.Controllers
{
    public class UsersController : ApiController
    {
        private EduardoPrimaveraContext db = new EduardoPrimaveraContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string salt = "PasswordHash";

            byte[] key = ASCIIEncoding.ASCII.GetBytes(salt);
            byte[] data = ASCIIEncoding.ASCII.GetBytes(user.Password);
            HMACSHA1 hMACSHA1 = new HMACSHA1(key);
            byte[] resultData = hMACSHA1.ComputeHash(data);
            string passHash = Convert.ToBase64String(resultData);
            byte[] hashByte = new byte[20];
            new Random().NextBytes(hashByte);
            byte[] session = hMACSHA1.ComputeHash(hashByte);
            string sessionHash = Convert.ToBase64String(session);

            var teste = db.Users.Where(b => b.Name == user.Name).SingleOrDefault();
            if (teste != null)
            {
                if (teste.Password == passHash)
                {
                    teste.Session = sessionHash;
                    db.Entry(teste).State = EntityState.Modified;
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        throw e;
                    }
                    return Ok(teste.Session);
                }
                else
                    return NotFound();
            }
            else
            {
                user.Password = passHash;
                user.Session = sessionHash;
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
            }
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}