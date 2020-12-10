using Ganss.XSS;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace EXCSLA.Shared.Blazor.Client
{
    public partial class Markdown
    {
        private string _content;

        [Inject] IHtmlSanitizer Sanitizer { get; set; }

        [Parameter]
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                HtmlContent = ConvertToHtml(value);
            }
        }

        public MarkupString HtmlContent { get; private set; }

        private MarkupString ConvertToHtml(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                // Convert markdown to Html using Markdig
                var html = Markdig.Markdown.ToHtml(content, new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());

                // Sanitize the Html to prevent malicious attacks
                var sanitizedHtml = Sanitizer.Sanitize(html);

                // return the Markupstring
                return new MarkupString(sanitizedHtml);
            }

            return new MarkupString();
        }
    }
}
