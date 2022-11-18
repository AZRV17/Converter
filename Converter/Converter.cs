using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Converter
{
	public class Param
	{
        public Param()
        {}
		public Param(string length, string width, string height)
		{
			this.length = length;
			this.width = width;
			this.height = height;
		}
		public string length;
		public string width;
		public string height;
	}
	// C:/Users/alexa/OneDrive/Рабочий стол/1.txt
	class Converter
	{
		public Converter()
		{
			
		}
		public void Print()
		{
			string format = "txt";
			Console.WriteLine("Введите путь к файлу");
			string Path = Console.ReadLine();
			Console.Clear();
			Console.WriteLine("F1 - сохранить файла | Esc - выйти");
			Console.WriteLine("------------------------------------------");
			if (Path.Contains(".json"))
			{
				DeserializeJson(Path);
				format = "json";
			}
			else if (Path.Contains(".txt"))
            {
				DeserializeTxt(Path);
				format = "txt";
			}
			else if (Path.Contains(".xml"))
			{
				DeserializeXml(Path);
				format = "xml";
			}
			ConsoleKeyInfo key = Console.ReadKey();
			switch (key.Key)
			{
				case ConsoleKey.F1:
					Serialize(Path, format);
					break;
				case ConsoleKey.Escape:
					Environment.Exit(0);
					break;
			}
		}
		private void Serialize(string Path, string format)
		{
			List<Param> Params = new List<Param>();
			string text = File.ReadAllText(Path);
			string[] lines = File.ReadAllLines(Path);
			int line = 0;
			if (format == "json")
			{
				Params = JsonConvert.DeserializeObject<List<Param>>(text);
			}
			else if (format == "txt")
			{
				for (int i = 0; i < lines.Length / 3; i++)
				{
					string length = File.ReadLines(Path).Skip(0 + line).First();
					string width = File.ReadLines(Path).Skip(1 + line).First();
					string height = File.ReadLines(Path).Skip(2 + line).First();
					Param param1 = new Param(length, width, height);
					Params.Add(param1);
					line += 3;
				}
			}
			else if (format == "xml")
            {
				XmlSerializer xml = new XmlSerializer(typeof(List<Param>));
				using (FileStream fs = new FileStream(Path, FileMode.Open))
				{
					Params = (List<Param>)xml.Deserialize(fs);
				}
			}
            
			Console.Clear();
			Console.WriteLine("Введите путь для сохранения файла");
			string path_to = Console.ReadLine();
			if (path_to.Contains(".json"))
			{
				string json = JsonConvert.SerializeObject(Params);
				File.WriteAllText(path_to, json);
			}
			else if (path_to.Contains(".xml"))
			{
				XmlSerializer xml = new XmlSerializer(typeof(List<Param>));
				using (FileStream fs = new FileStream(path_to, FileMode.OpenOrCreate))
				{
					xml.Serialize(fs, Params);
				}
			}
			else if (path_to.Contains(".txt"))
			{
				foreach (Param param in Params)
                {
					File.AppendAllText(path_to, param.length + "\n");
					File.AppendAllText(path_to, param.height + "\n");
					File.AppendAllText(path_to, param.width + "\n");
                }
			}
		}
		static void DeserializeJson(string Path)
        {
			string text = File.ReadAllText(Path);
			List<Param> Params = new List<Param>();	
			List<Param> conv = JsonConvert.DeserializeObject<List<Param>>(text);
			foreach (Param i in conv)
            {
				Console.WriteLine(i.length);
                Console.WriteLine(i.width);
				Console.WriteLine(i.height);
				Params.Add(i);
			}
            //else if (Path.Contains(".xml"))
            //{
            //	XmlSerializer xml = new XmlSerializer(typeof(List<Param>));
            //	using (FileStream fs = new FileStream(path_to, FileMode.OpenOrCreate))
            //	{
            //		xml.Serialize(fs, Params);
            //	}
            //}
            //else if (Path.Contains(".txt"))
            //{
            //	File.WriteAllText(Path, Convert.ToString(Params));
            //}
            //break;
        }
		static void DeserializeTxt(string Path)
        {
			string text = File.ReadAllText(Path);
			Console.WriteLine(text);
        }
		static void DeserializeXml(string Path)
        {
			List<Param> i;
			List<Param> Params = new List<Param>();
			XmlSerializer xml = new XmlSerializer(typeof(List<Param>));
            using (FileStream fs = new FileStream(Path, FileMode.Open))
            {
                i = (List<Param>)xml.Deserialize(fs);
            }
			foreach (var n in i)
            {
				Console.WriteLine(n.length);
				Console.WriteLine(n.width);
				Console.WriteLine(n.height);
				Params.Add(n);
			}
        }
	}
}
