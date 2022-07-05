
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public class ValuteData
{

    private ValuteDataValuteCursOnDate[] valuteCursOnDateField;

    private uint onDateField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ValuteCursOnDate")]
    public ValuteDataValuteCursOnDate[] ValuteCursOnDate
    {
        get
        {
            return this.valuteCursOnDateField;
        }
        set
        {
            this.valuteCursOnDateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint OnDate
    {
        get
        {
            return this.onDateField;
        }
        set
        {
            this.onDateField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public class ValuteDataValuteCursOnDate
{

    private string vnameField;

    private ushort vnomField;

    private decimal vcursField;

    private ushort vcodeField;

    private string vchCodeField;

    /// <remarks/>
    public string Vname
    {
        get
        {
            return this.vnameField;
        }
        set
        {
            this.vnameField = value;
        }
    }

    /// <remarks/>
    public ushort Vnom
    {
        get
        {
            return this.vnomField;
        }
        set
        {
            this.vnomField = value;
        }
    }

    /// <remarks/>
    public decimal Vcurs
    {
        get
        {
            return this.vcursField;
        }
        set
        {
            this.vcursField = value;
        }
    }

    /// <remarks/>
    public ushort Vcode
    {
        get
        {
            return this.vcodeField;
        }
        set
        {
            this.vcodeField = value;
        }
    }

    /// <remarks/>
    public string VchCode
    {
        get
        {
            return this.vchCodeField;
        }
        set
        {
            this.vchCodeField = value;
        }
    }
}
