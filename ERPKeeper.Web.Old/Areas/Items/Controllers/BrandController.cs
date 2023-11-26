using ERPKeeper.Node.Models.Items;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Items.Controllers
{

    public class BrandController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.ItemBrands.Find(id));
        public ActionResult Items(Guid id) => View(Organization.ItemBrands.Find(id));

        public ActionResult Delete(Guid id)
        {
            Organization.ItemBrands.Delete(id);
            return RedirectToAction("Home", "Brands");
        }

        public ActionResult Save(Brand brand)
        {
            var existBrand = Organization.ItemBrands.Find(brand.Uid);

            if (existBrand != null)
                existBrand.Update(brand);
            else if (brand.Name.Trim() != string.Empty)
                Organization.ItemBrands.CreateNew(brand);

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public ActionResult UploadImage(Guid Uid)
        {
            var image = System.Web.Helpers.WebImage.GetImageFromRequest();
            var existBrand = Organization.ItemBrands.Find(Uid);

            if (image != null && existBrand != null)
            {
                byte[] toPutInDb = image.GetBytes();

                existBrand.Image = toPutInDb;
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);

        }



        [OutputCache(Duration = 3600, VaryByParam = "id")]
        public ActionResult GetImage(Guid id)
        {
            var item = Organization.ItemBrands.Find(id);

            if (item != null && item.Image != null)
            {
                System.Web.Helpers.WebImage image = new System.Web.Helpers.WebImage(item.Image);
                return File(image.GetBytes(), "image/" + image.ImageFormat, image.FileName);
            }

            System.Web.Helpers.WebImage defaultimage = new System.Web.Helpers.WebImage(Server.MapPath("/Content/Images/default_product.jpg"));
            return File(defaultimage.GetBytes(), "image/" + defaultimage.ImageFormat, defaultimage.FileName);

        }


    }
}