using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace _03_Tubo_Read_TMX_MEM
{
    class Program
    {
        // de https://stackoverflow.com/questions/40090098/deserialize-xml-where-string-may-contain-xml-html
        static void Main(string[] args)
        {
            Console.WriteLine("03_Tubo_Read_TMX_ME (c) 2021 mcanals MIT License");
            bool commandlineerrors = false ;
            string inpfileORI = "none";
            int splitsize = 0;

            if (args.Length== 2)
            {
                inpfileORI= args[0].Trim();
                if (!File.Exists(inpfileORI))
                {
                    Console.WriteLine("Looks like TMX file '{0}' does not exist", inpfileORI);
                    commandlineerrors = true;
                } else
                {
                    if (inpfileORI.Length > 4 )
                    {
                        if (inpfileORI.Substring(inpfileORI.Length-4).ToUpper() != ".TMX")
                        {
                            Console.WriteLine("Looks like TMX file '{0}' has not a valid name (must end with '.TMX')", inpfileORI);
                            commandlineerrors = true;
                        }
                    } else
                    {
                        // less than 4 chars
                        Console.WriteLine("Looks like TMX file '{0}' has not a valid name (should be at least 5 char filename as 'n.TMX')",inpfileORI);
                        commandlineerrors = true;
                    }
                }
                // now the number
                if (!int.TryParse(args[1], out splitsize))
                {
                    Console.WriteLine("Cannot convert '{0}' to an int", args[1]);
                    commandlineerrors = true;
                }
            } else
            {
                Console.WriteLine("You need to specify 2 parameters. Number of detected parameters is '{0}'", args.Length);
                commandlineerrors = true;
            }


            if (commandlineerrors)
            {
                Console.WriteLine("Format or input not valid. Correct format: ");
                Console.WriteLine("03_Tubo_READ_TMX_MEM path\\file.TMX integer_number_to_split");
                return;
            }

            Console.WriteLine("File  -> {0}", inpfileORI);
            Console.WriteLine("Split -> {0}", splitsize);




            // instead of directly serialize we need to use XMLWriter settins
            // otherwise origina CR/LF in CDTA will be converted in LF
            // as this is the default behaivor for XML
            // https://stackoverflow.com/questions/60976792/net-core-3-1-soap-platform-not-supported-error-compiling-jscript-csharp-script
            XmlSerializer ser = new XmlSerializer(typeof(tmx));
            XmlWriterSettings x_settings = new XmlWriterSettings();

            // x_settings.NewLineChars = Environment.NewLine;
            // x_settings.NewLineOnAttributes = true;
            // x_settings.NewLineHandling = NewLineHandling.Replace;
            // x_settings.CloseOutput = true;
            x_settings.Indent = true; // this is what we need
            // x_settings.NewLineHandling = NewLineHandling.Entitize;
            // x_settings.NewLineOnAttributes = true;
            XmlWriter x_write;
            
            //  inpfileORI =  @"C:\u\usr\pro\VS2019\test\00_data\TEST.TMX";
            //inpfileORI = @"C:\u\usr\pro\VS2019\test\00_data\MFP20ABD030_SPA.TMX";            
            //inpfileORI = @"C:\u\usr\pro\VS2019\test\00_data\_ABD004.TMX";
            //inpfileORI = @"C:\u\usr\pro\VS2019\test\00_data\LEGAL_UTF-8.TMX";
            string inpfile = inpfileORI.Replace(".TMX", "_CDATA.TMX");
            // inpfile = @"C:\u\usr\pro\VS2019\test\00_data\BM150ABD005_SPA.TMX";
            // me he quedado aquí. El problema es que los TMX contienen tags <ph> que
            // no son válidos en un xml y la serialización falla. La solución es poner
            // un evento que se desencandena cuando encuentra un elemento desconocido
            // (desonocido pq lo quitamos de la definición de la clase). De esa manera
            // he conseguido leer los tags. El problema es que al serializar de nuevo
            // como no son válidos me los resuelve a la hora de escribir la nueva clasa
            // la solución creo más correcta es preprocesar el tmx para qu sea cdata
            // pero es pues un apaño y ya no quiero perder más tiempo.
            // Además los tmx cambian la traducción.
            // me he quedado pues en el preproceso
            string line = "";
            bool insideCDATA = false;

            // we need to read the encoding of the TMX file (otherwise
            // serailization will fail
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            using (var reader = new StreamReader(inpfileORI))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string auxS = line.ToUpper();       
                    if (auxS.Contains("UTF-16")){
                        encoding = System.Text.Encoding.Unicode;                        
                    }
                    break;
                }
            }
            x_settings.Encoding = encoding;
                
                             
            using (StreamWriter writer = new StreamWriter(inpfile,false, encoding))
            {
                using (var reader = new StreamReader(inpfileORI,encoding))
                {
                    while ((line = reader.ReadLine()) != null)
                    {

                        //Console.WriteLine(line);
                        if (insideCDATA == false)
                        {
                            // not in CDDATA
                            string iniseg = "<seg>";
                            string iniCDATA = "<![CDATA[";
                            string auxlin = line.TrimStart();
                            if (auxlin.Length >= iniseg.Length) // at least <seg> lenght
                            {
                                if (iniseg == auxlin.Substring(0, iniseg.Length))
                                {
                                    // found segment but before doing it lets make
                                    // sure no CDATA is already there 
                                    bool ContainsCDATA = false;
                                    if (auxlin.Length > iniseg.Length + "[CDATA[".Length)
                                    {
                                        ; ContainsCDATA = auxlin.Contains("[CDATA[");
                                    }
                                    // we cannot add CDATA if already exists
                                    if (ContainsCDATA == false) // we can add
                                    {
                                        line = line.Replace(iniseg, iniseg + iniCDATA);
                                        insideCDATA = true; // inside CDTA
                                    }



                                }
                            }
                        }
                        if (insideCDATA == true) // we have to close CDTA
                        {
                            string auxlin = line.TrimStart();
                            string finseg = "</seg>";
                            string finCDATA = "]]>";
                            if (auxlin.Length >= finseg.Length) // min lenght
                            {
                                if (finseg == auxlin.Substring(auxlin.Length - finseg.Length))
                                { // encontrado
                                    line = line.Replace(finseg, finCDATA + finseg);
                                    insideCDATA = false;
                                }

                            }
                        }
                        writer.WriteLine(line);
                    }
                }
            }


            string outfile = inpfile.Replace("_CDATA.TMX", "_CDATA_{0}.TMX");
            // tmx myTMX = ser.Deserialize(new FileStream(inpfile, FileMode.Open,FileAccess.Read, FileShare.Read)) as tmx;
            tmx myTMX;
            using ( Stream stream = new FileStream(inpfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                myTMX = (tmx)ser.Deserialize(stream); 
            }
            

            int recordtotal = 0;
            int recordsplit = -1;
            int record = -1;
            List<String> Splittedfiles = new List<String>();

            if (myTMX != null)
            {
                Console.WriteLine("TMX file {0} is valid", inpfile);
            }

            tmx myOutTMX = ser.Deserialize(new FileStream(inpfile, FileMode.Open, FileAccess.Read, FileShare.Read)) as tmx;

            recordtotal = myTMX.body.GetLength(0)-1; // first dimension number of items
            var splitlist = new List<tmxTU>();


            while (record < recordtotal)
            {
                // new record
                record += 1;
                recordsplit += 1;
                splitlist.Add(myTMX.body[record]);
                if (recordsplit >= splitsize - 1)
                {
                    // flush
                    myOutTMX.body = splitlist.ToArray();

                    using (Stream stream = new FileStream(string.Format(outfile, record + 1), FileMode.Create, FileAccess.Write))
                    {
                        // ser.Serialize(stream, myOutTMX);
                        x_write = XmlWriter.Create(stream, x_settings);
                        ser.Serialize(x_write, myOutTMX);
                        x_write.Close();
                    }
                    // this only closses at the end. 
                    //ser.Serialize(new FileStream(string.Format(outfile, record + 1), FileMode.Create, FileAccess.Write), myOutTMX);
                    splitlist = new List<tmxTU>();
                    recordsplit = -1;
                    Splittedfiles.Add(string.Format(outfile, record + 1));
                }
            }
                if (recordsplit != -1)
                {
                    // final chunk as records
                    myOutTMX.body = splitlist.ToArray();
                    using (Stream stream = new FileStream(string.Format(outfile, record + 1), FileMode.Create, FileAccess.Write))
                    {

                        // ser.Serialize(stream, myOutTMX);
                        x_write = XmlWriter.Create(stream, x_settings);
                        ser.Serialize(x_write, myOutTMX);
                        x_write.Close();
                    }
                    Splittedfiles.Add(string.Format(outfile, record + 1));

                    // ser.Serialize(new FileStream(string.Format(outfile, record+1), FileMode.Create, FileAccess.Write), myOutTMX);
                }
            
            // final conversion

            foreach (string SplittedCDATAfile in Splittedfiles)
                {
                // like C:\u\usr\pro\VS2019\test\00_data\_ABD004_CDATA_2029.TMX
                string Splittedfile = SplittedCDATAfile.Replace("_CDATA", "");
                // need to read CDATA and Write removing CDATA


                using (var writer = new StreamWriter(Splittedfile))
                {
                    using (var reader = new StreamReader(SplittedCDATAfile))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            //Console.WriteLine(line);
                            if (insideCDATA == false)
                            {
                                // not in CDDATA
                                string iniseg = "<seg><![CDATA[";
                                string newiniseg = "<seg>";
                                string auxlin = line.TrimStart();
                                if (auxlin.Length >= iniseg.Length) // at least <seg><![CDATA[ lenght
                                {
                                    if (iniseg == auxlin.Substring(0, iniseg.Length))
                                    {
                                        line = line.Replace(iniseg, newiniseg);
                                        insideCDATA = true; // inside CDTA
                                        
                                    }
                                }
                            }
                            if (insideCDATA == true) // we have to close CDTA
                            {
                                string auxlin = line.TrimStart();
                                string finseg = "]]></seg>";
                                string newfinseg = "</seg>";
                                if (auxlin.Length >= finseg.Length) // min lenght
                                {
                                    if (finseg == auxlin.Substring(auxlin.Length - finseg.Length))
                                    { // encontrado
                                        line = line.Replace(finseg, newfinseg);
                                        insideCDATA = false;
                                    }

                                }
                            }
                            writer.WriteLine(line);
                        }
                    }
                }



            }

            Console.WriteLine("EOP!");


        }
    }
}
