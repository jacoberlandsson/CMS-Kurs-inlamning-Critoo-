using Critoo.Models;
using Critoo.Services;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Critoo.Controllers
{
    public class ContactController : SurfaceController
    {
        public ContactController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        public IActionResult Index(ContactForm contactForm)
        {
            if(!ModelState.IsValid)
                return CurrentUmbracoPage();

            using var email = new MailSenderService("no-reply@crito.com", "smtp.crito.com", 587, "contact@crito.com", "BytMig123!");

            email.SendEmailAsync(contactForm.Email, "We have received your message", "We have received your message and will get back to you asap").ConfigureAwait(false);
            email.SendEmailAsync("umbraco@crito.com", $"{ contactForm.Name} sent a message", contactForm.Message ).ConfigureAwait(false);

            return LocalRedirect(contactForm.RedirectURL ?? "/");


            
        }
    }
}
