using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace BonusRoll
{
	class MainClass
	{
		private static string storageLocation = "Dropbox/SYNC/apps/BonusRoll/BonusRollData.xml";
		public static List<Subject> SubjectList;

		static void Main (string[] args)
		{
			NSApplication.Init ();
			if (File.Exists (getStoragePath ())) {
				Console.Out.WriteLine ("Existing data found, loading existing data..");
				SubjectList = readFromDisk ();
			} else {
				Console.Out.WriteLine ("No existing data found, loading initial data..");
				SubjectList = loadInitalContent ();
				SaveToDisk ();
			}

			NSApplication.Main (args);
		}

		public static void SaveToDisk ()
		{
			string savePath = Path.Combine (Directory.GetCurrentDirectory (), getStoragePath ());

			var ds = new DataContractSerializer (typeof(List<Subject>));
			var xmlsettings = new XmlWriterSettings { Indent = true };
			using (var w = XmlWriter.Create (savePath, xmlsettings))
				ds.WriteObject (w, SubjectList);
		}

		private static List<Subject> readFromDisk ()
		{
			DataContractSerializer ds = new DataContractSerializer (typeof(List<Subject>));
			FileStream fs = new FileStream (getStoragePath (), FileMode.Open);
			XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader (fs, new XmlDictionaryReaderQuotas ());
			List<Subject> list = (List<Subject>)ds.ReadObject (reader);
			reader.Close ();
			fs.Close ();
			return list;
		}

		private static string getStoragePath ()
		{
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			return Path.Combine (documents, storageLocation);
		}

		private static List<Subject> loadInitalContent ()
		{
			List<Subject> list = new List<Subject> ();
			list.Add (new Subject ("CompB"));
			list.Add (new Subject ("MsTe"));
			list.Add (new Subject ("Ph1M"));

			list.Add (new Subject ("IntTe"));
			list.Add (new Subject ("PmQm"));
			list.Add (new Subject ("AppArch"));

			list.Add (new Subject ("An1I"));
			list.Add (new Subject ("Prog3"));

			return list;
		}
	
	}
}

