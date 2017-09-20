using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeMyDay.TagHelpers
{
	[HtmlTargetElement(Attributes = "menu-link")]
    public class MenuTagHelper : TagHelper
    {
	    public override void Process(TagHelperContext context, TagHelperOutput output)
	    {
			base.Process(context, output);
			var classAttr = new TagHelperAttribute("class", "active");
		    output.Attributes.Add(classAttr);
			//output.Attributes.SetAttribute("class", "active");
	    }
    }
}
