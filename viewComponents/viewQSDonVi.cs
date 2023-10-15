using Microsoft.AspNetCore.Mvc;


namespace WebForQLQS.viewComponents
{
    public class viewQSDonVi: ViewComponent
    {
        public IViewComponentResult Invoke() { 
        
            return View();
        
        }


    }

    public class viewAddInf : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();

        }


    }


    public class viewForAnalyst : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();

        }


    }
}
