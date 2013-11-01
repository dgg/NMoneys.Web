using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI;
using HtmlTags;

namespace NMoneys.Web.Views
{
	public static class HtmlHelperExtensions
	{
		public static HtmlTag NavEntry<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string linkText, string classIfCurrent = "active") where TController : Controller
		{
			RouteValueDictionary values = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);

			HtmlTag li = HtmlTextWriterTag.Li.asTag()
				.AddClassIf(isCurrentAction(helper, values), classIfCurrent)
				.AppendHtml(
				   helper.RouteLink(linkText, values).ToHtmlString());

			return li;
		}

		private static HtmlTag asTag(this HtmlTextWriterTag tag)
		{
			return new HtmlTag(tag.ToString());
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

		public static HtmlTag IconNavEntry<TController>(this HtmlHelper helper, Expression<Action<TController>> action, string iconClass, string classIfCurrent = "active") where TController : Controller
		{
			RouteValueDictionary values = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);

			string url = UrlHelper.GenerateUrl(null, null, null, null, null, null, values, helper.RouteCollection, helper.ViewContext.RequestContext, false);

			var li = new HtmlTag("li")
				.AddClassIf(isCurrentAction(helper, values), classIfCurrent)
				.Append(
					new LinkTag(string.Empty, url).Append(
						new HtmlTag("span").AddClass(iconClass)
					)
				);
			return li;
		}

		public static HtmlTag AddClassIf(this HtmlTag tag, bool condition, string className)
		{
			if (condition)
			{
				tag.AddClass(className);
			}
			return tag;
		}

		public static HtmlTag ModelInfo<T>(this HtmlHelper<T> helper, Expression<Func<T, object>> lambda, string placement = "right", string cssClass = null, string tag = "span")
		{
			string title =  helper.ViewData.description(lambda);
		
			var span = new HtmlTag("span")
				.AddClass("show-info")
				.AddClassIf(!string.IsNullOrEmpty(cssClass), cssClass)
				.Attr("title", title)
				.Attr("data-placement", placement)
				.Encoded(true)
				.Text(lambda.Compile()(helper.ViewData.Model).ToString());

			return span;
		}

		private static string description<T>(this ViewDataDictionary<T> model, Expression<Func<T, object>> lambda)
		{
			string description = ModelMetadata.FromLambdaExpression(lambda, model).Description;
			if (string.IsNullOrEmpty(description)) description = model.name(lambda);
			return description;
		}

		private static string name<T>(this ViewDataDictionary<T> model, Expression<Func<T, object>> lambda)
		{
			string description = ModelMetadata.FromLambdaExpression(lambda, model).PropertyName;
			return description;
		}
	}
}