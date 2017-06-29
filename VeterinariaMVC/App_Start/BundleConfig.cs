using System.Web;
using System.Web.Optimization;

namespace VeterinariaMVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Assets/js/modernizr").Include(
                        "~/Assets/js/modernizr-2.6.2.js",
                        "~/Assets/js/respond.min.js"));

            bundles.Add(new StyleBundle("~/Assets/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/font-awesome/font-awesome.min.css",
                      "~/Assets/css/style.css"));

            bundles.Add(new ScriptBundle("~/Assets/js").Include(
                      "~/Assets/js/bootstrap.min.js",
                      "~/Assets/js/jquery.validate.min.js",
                      "~/Assets/js/jquery.validate.unobtrusive.min.js",
                      "~/Assets/js/jquery.form.min.js",
                      "~/Assets/js/funciones.js"));

        }
    }
}
