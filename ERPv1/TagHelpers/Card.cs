using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.TagHelpers
{
    [HtmlTargetElement("card")]
    public class Card : TagHelper
    {
        public string HeaderBackGround { get; set; }
        public string TextColor { get; set; }
        public string Title { get; set; }
        public string AddationalClass { get; set; }

        public string AddationlInfo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string preContent = @"
                       
                        <div class='card-header {0} '>
                            <b class='{1}'>
                                <span class='fa {2}'> </span>&nbsp; {3} &nbsp;{4}
                            </b>
                        </div>
                        <div class='card-body'>
            ";
            string postContent = @"
            </div>
            
            ";
            output.TagName = "div";

            output.Attributes.Add("class", "card mt-3");
            output.PreContent.SetHtmlContent(
                string.Format(preContent, HeaderBackGround, TextColor, AddationalClass, Title, AddationlInfo));

            output.PostContent.SetHtmlContent(postContent);

        }
    }
}
