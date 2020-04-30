using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pachyderm.Services.UI
{
	public class ModalService
	{
		public event Action<string, RenderFragment> OnShow;
		public event Action OnClose;

		public void Show(string title, Type contentType, object model = null)
		{
			if (contentType.BaseType != typeof(ComponentBase))
			{
				throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
			}

			var content = new RenderFragment(x => { 
				x.OpenComponent(0, contentType);
				if (model != null)
					x.AddAttribute(1, "Model", model);
				x.CloseComponent();
			});
			OnShow?.Invoke(title, content);
		}

		public void Close()
		{
			OnClose?.Invoke();
		}
	}
}
