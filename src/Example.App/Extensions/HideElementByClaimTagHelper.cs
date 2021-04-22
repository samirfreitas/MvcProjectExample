using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.App.Extensions
{
    [HtmlTargetElement("*",Attributes = "supress-by-claim-name")]
    [HtmlTargetElement("*",Attributes = "supress-by-claim-value")]
    public class HideElementByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        [HtmlAttributeName("supress-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public HideElementByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if(output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            var haveAccess = CustomAuthorization.ValidarClaimsUsuario(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);
            if (haveAccess) return;

            output.SuppressOutput();

        }

    }



    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class HideElementByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;    

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        [HtmlAttributeName("supress-by-claim-value")]
        public string IdentityClaimValue { get; set; }

        public HideElementByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
           
        }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            //preciso identificar a forma de saber em qual action estou
            var haveAction = _contextAccessor.HttpContext.GetEndpoint();
           
            if (true) return;

            output.SuppressOutput();

        }

    }
}
