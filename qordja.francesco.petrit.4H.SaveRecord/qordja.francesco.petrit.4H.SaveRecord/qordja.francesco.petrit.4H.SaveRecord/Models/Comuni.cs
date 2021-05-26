using System;

public class Class1
{
	public Class1()
	{

    public int ID { get; set; }
    private string _nomeComune;
    public string NomeComune
    {
        get => _nomeComune;

        set
        {
           
            if (value.Length == 22)
                _nomeComune = value;

            if (value.Length < 22)
                value = value.PadRight(22);
            else if (value.Length > 22)
                value = value.Substring(0, 22);

            _nomeComune = value;
        }
    }
    private string _codiceCatastale;

    public string CodiceCatastale
    {
        get => _codiceCatastale;

        set
        {
            if (value.Length == 4)
                _codiceCatastale = value;

            if (value.Length < 4)
                value = value.PadRight(4);
            else if (value.Length > 4)
                value = value.Substring(0, 4);

            _codiceCatastale = value;
        }
    }

    public Comune() { }

    public Comune(string riga, int id)
    {
        string[] colonne = riga.Split(',');
        ID = id;
        CodiceCatastale = colonne[0];
        NomeComune = colonne[1];
    }

    public override string ToString() => $"{ID}, {CodiceCatastale}, {NomeComune}";
}

public class Comuni : List<Comune> 
{
    public string NomeFile { get; }

    public Comuni() { }

    public Comuni(string fileName)
    {
        NomeFile = fileName;

        using (FileStream fin = new FileStream(fileName, FileMode.Open))
        {
            StreamReader reader = new StreamReader(fin);

            int id = 1;

            while (!reader.EndOfStream)
            {
                string riga = reader.ReadLine();
                Comune c = new Comune(riga, id++);
                Add(c);
            }
        }
    }

    public void Save()
    {
        string fileName = NomeFile.Split('.')[0] + ".bin";
        Save(fileName);
    }

    public void Save(string fileName)
    {
        using (FileStream fout = new FileStream(fileName, FileMode.Create))
        {
            BinaryWriter writer = new BinaryWriter(fout);

            foreach (Comune comune in this)
            {
                writer.Write(comune.ID);
                writer.Write(comune.CodiceCatastale);
                writer.Write(comune.NomeComune);
            }
            writer.Flush();
            writer.Dispose();
        }
    }

    public void Load()
    {
        string fn = NomeFile.Split('.')[0] + ".bin";
        Load(fn);
    }

    public void Load(string fileName)
    {
        Clear(); 

        using (FileStream fin = new FileStream(fileName, FileMode.Open))
        {
            BinaryReader reader = new BinaryReader(fin);
            Comune c = new Comune();

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                // Leggo l'ID
                c.ID = reader.ReadInt32();
                // Leggo il codice catastale
                c.CodiceCatastale = reader.ReadString();
                // Leggo il nome del comune
                c.NomeComune = reader.ReadString();
                Add(c);
            }
        }
    }

    public Comune RicercaComune(int index)
    {
        string fn = NomeFile.Split('.')[0] + ".bin";
        return RicercaComune(index, fn);
    }

    
    public Comune RicercaComune(int index, string fileName)
    {
        FileStream fin = new FileStream(fileName, FileMode.Open);
        BinaryReader reader = new BinaryReader(fin);

        fin.Seek((index - 1) * 32, SeekOrigin.Begin);
        Comune c = new Comune();
        c.ID = reader.ReadInt32();
        c.CodiceCatastale = reader.ReadString();
        c.NomeComune = reader.ReadString();

        return c;
    }

	
}
