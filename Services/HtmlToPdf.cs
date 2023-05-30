using System;
using iText.Html2pdf;

namespace amazon.Services{
    
    public class HtmlToPdf
    {
        public void ConvertHtmlToPdf(string html, string dest)
        {
            HtmlConverter.ConvertToPdf(html, new FileStream(dest, FileMode.Create));
        }
    }

    
}

