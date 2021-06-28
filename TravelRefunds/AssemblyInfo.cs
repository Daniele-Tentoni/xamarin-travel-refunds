using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportFont("fa-regular-400.ttf", Alias = "far")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "fas")]
[assembly: ExportFont("fa-brands-400.ttf", Alias = "fab")]

[assembly: NeutralResourcesLanguage("en-US")]