using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp36.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			builder.Services.AddSingleton<Svasta>();
			await builder.Build().RunAsync();
		}
	}

	public delegate void Poziv(Test t);

	public class Test
	{
		public string Ime { get; set; }
		public string Prezime { get; set; }
	}

	public class Svasta
	{
		private Test _t = new Test();
		public Test t 
		{
			get => _t;
			set
			{
				_t = value;
				onPromena?.Invoke();
			}
		} 

		public event Action onPromena;

		public void Promena()
		{
			onPromena?.Invoke();
		}
	}
}
