using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kaoru_Art_Gallery.Pages
{
    public class LanguageModel : PageModel
    {
        // used to set the language property in view
        private bool clicked;

        LanguageModel(bool clicked) => Clicked = clicked;

        public async Task<IActionResult> OnPostChangeLanguage(bool clicked)
        {
            isClicked(clicked);
            return Page();
        }

        private void isClicked(bool clicked)
        {
            if (clicked)
            {

            }
            else
            {

            }
        }
        public bool Clicked
        {
            get => clicked;
            set => clicked = value;
        }
        public void OnGet()
        {
        }
    }
}
