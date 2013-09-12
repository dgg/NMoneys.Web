using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;

namespace NMoneys.Web.Views
{
	public static class HtmlHelperExtensions
	{
		 public static IHtmlString NavEntry<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string linkText, string classIfCurrent = "active") where TController : Controller
		 {
			 RouteValueDictionary values = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);

			 TagBuilder li = HtmlTextWriterTag.Li.asTag(tb => 
				 tb.InnerHtml = helper.RouteLink(linkText, values).ToHtmlString());
			 if (isCurrentAction(helper, values))
			 {
				 li.AddCssClass(classIfCurrent);
			 }

			 return new HtmlString(li.ToString(TagRenderMode.Normal));
		 }

		private static TagBuilder asTag(this HtmlTextWriterTag tag, Action<TagBuilder> building = null)
		{
			var builder = new TagBuilder(tag.ToString().ToLower());
			if (building != null) building(builder);
			return builder;
		}

		enum Key
		{
			controller,
			action
		}

		private static bool isCurrentAction(HtmlHelper helper, RouteValueDictionary values)
		{
			string currentController = helper.getValue(Key.controller),
				controller = values.getValue(Key.controller),
				currentAction = helper.getValue(Key.action),
				action = values.getValue(Key.action);

			bool isCurrent = StringComparer.OrdinalIgnoreCase.Equals(currentController, controller) &&
				StringComparer.OrdinalIgnoreCase.Equals(currentAction, action);

			return isCurrent;
		}

		private static string getValue(this HtmlHelper helper, Key key)
		{
			return helper.ViewContext.Controller.ValueProvider.GetValue(key.ToString())
				.RawValue.ToString();
		}

		private static string getValue(this RouteValueDictionary values, Key key)
		{
			return values[key.ToString()].ToString();
		}

		public static IHtmlString IconNavEntry<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string iconClass, string classIfCurrent = "active") where TController : Controller
		{
			RouteValueDictionary values = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);
			
			string url = UrlHelper.GenerateUrl(null, null, null, null, null, null, values, helper.RouteCollection, helper.ViewContext.RequestContext, false);
			TagBuilder anchor = HtmlTextWriterTag.A.asTag(tb => tb.MergeAttribute("href", url));

			TagBuilder span = HtmlTextWriterTag.Span.asTag(tb => tb.AddCssClass(iconClass));
			anchor.InnerHtml = span.ToString(TagRenderMode.Normal);

			TagBuilder li = HtmlTextWriterTag.Li.asTag(tb =>
				tb.InnerHtml = anchor.ToString(TagRenderMode.Normal));

			if (isCurrentAction(helper, values))
			{
				li.AddCssClass(classIfCurrent);
			}

			return new HtmlString(li.ToString(TagRenderMode.Normal));
		}
	}
}