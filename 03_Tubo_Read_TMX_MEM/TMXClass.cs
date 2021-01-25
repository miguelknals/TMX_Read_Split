using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace _03_Tubo_Read_TMX_MEM
{
 
        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class tmx 
        {

            private tmxHeader headerField;

            private tmxTU[] bodyField;

            private decimal versionField;

        


            /// <remarks/>
            public tmxHeader header
            {
                get
                {
                    return this.headerField;
                }
                set
                {
                    this.headerField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("tu", IsNullable = false)]
            public tmxTU[] body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class tmxHeader
        {

            private string creationtoolversionField;

            private string segtypeField;

            private string adminlangField;

            private string srclangField;

            private string otmfField;

            private string creationtoolField;

            private string datatypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string creationtoolversion
            {
                get
                {
                    return this.creationtoolversionField;
                }
                set
                {
                    this.creationtoolversionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string segtype
            {
                get
                {
                    return this.segtypeField;
                }
                set
                {
                    this.segtypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string adminlang
            {
                get
                {
                    return this.adminlangField;
                }
                set
                {
                    this.adminlangField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string srclang
            {
                get
                {
                    return this.srclangField;
                }
                set
                {
                    this.srclangField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("o-tmf")]
            public string otmf
            {
                get
                {
                    return this.otmfField;
                }
                set
                {
                    this.otmfField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string creationtool
            {
                get
                {
                    return this.creationtoolField;
                }
                set
                {
                    this.creationtoolField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string datatype
            {
                get
                {
                    return this.datatypeField;
                }
                set
                {
                    this.datatypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class tmxTU
        {

            private tmxTUProp[] propField;

            private tmxTUTuv[] tuvField;

            private int tuidField;

        private string datatypeField;

            private string creationdateField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("prop")]
            public tmxTUProp[] prop
            {
                get
                {
                    return this.propField;
                }
                set
                {
                    this.propField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("tuv")]
            public tmxTUTuv[] tuv
            {
                get
                {
                    return this.tuvField;
                }
                set
                {
                    this.tuvField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public int tuid
            {
                get
                {
                    return this.tuidField;
                }
                set
                {
                    this.tuidField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
        public string datatype
        {
            get
            {
                return this.datatypeField;
            }
            set
            {
                this.datatypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
            public string creationdate
            {
                get
                {
                    return this.creationdateField;
                }
                set
                {
                    this.creationdateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class tmxTUProp
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class tmxTUTuv
        {

            private tmxTUTuvProp propField;
            
            private XmlCDataSection segField; 

            private string langField;

            /// <remarks/>
            public tmxTUTuvProp prop
            {
                get
                {
                    return this.propField;
                }
                set
                {
                    this.propField = value;
                }
            }

            /// <remarks/>
            // [System.Xml.Serialization.XmlIgnore] // tag ignore
            [System.Xml.Serialization.XmlElement("seg", typeof (XmlCDataSection))]
            public XmlCDataSection seg 
            {
                get
                {
                    return this.segField;
                }
                set
                {
                    this.segField = value;
                }
            }

            

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
            public string lang
            {
                get
                {
                    return this.langField;
                }
                set
                {
                    this.langField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class tmxTUTuvProp
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }














}
