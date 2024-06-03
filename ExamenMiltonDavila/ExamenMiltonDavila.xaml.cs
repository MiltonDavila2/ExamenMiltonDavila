

namespace ExamenMiltonDavila;

public partial class ExamenMiltonDavila : ContentPage
{
	public ExamenMiltonDavila()
	{
		InitializeComponent();
	}


	public void CambioDineroMD(object sender, CheckedChangedEventArgs e)
	{
		if (e.Value)
		{
			var botonRadio = sender as RadioButton;

			LabelSeleccionadoMD.Text = $"Selecciono la recarga de {botonRadio.Content} $";


		}

	}

	private int ObtenerNumeroRecarga()
	{
		if(Boton3.IsChecked == true)
		{
			return 3;
		}else if(Boton5.IsChecked == true)
		{
			return 5;
		}else if(Boton10.IsChecked == true)
		{
			return 10;
		}
		

		return 0;
	}

	public async void Confirmacion(object sender, EventArgs e)
	{
		var NumeroIngresado = EntradaNumeroMD.Text;
		var OperadorSeleccionado = EscojerOperadorMD.SelectedItem?.ToString();
		var RecargaElegida =  ObtenerNumeroRecarga();
		var fecha = DateTime.Now;

		if(string.IsNullOrEmpty(NumeroIngresado))
		{
			await DisplayAlert("Error", "No ha ingresado un numero de telefono, intentelo de nuevo", "OK");
			return;
		}
		

		var confirmacion = await DisplayAlert("Confirmacion", $"Desea realizar la recarga de {RecargaElegida}$ de la operadora {OperadorSeleccionado} al telefono {NumeroIngresado} en la fecha {fecha}", "Confirmar","Cancelar");

		if (confirmacion)
		{

			await DisplayAlert("Listo", "Se ha hecho la recarga (Se ha guardado en la memoria cache la informacion o vea el metodo Envio de datos)", "Ok");
			await EnvioDeDatos(NumeroIngresado,OperadorSeleccionado,RecargaElegida,fecha);
		}

	}


	public async Task EnvioDeDatos(string Numero, string Operadora, int Recarga, DateTime tiempo)
	{
		string nombre = $"{Numero}.txt";
		string texto = $"Se realizo una recarga\nTelefono = {Numero}\nOperadora={Operadora}\n Cantidad Recarga ={Recarga} \nFecha Recarga = {tiempo}";
		string file = Path.Combine(FileSystem.Current.CacheDirectory, nombre);
		File.WriteAllText(file, texto);

	}


	
}