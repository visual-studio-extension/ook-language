using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OokLanguage.Tag
{

    public class OokTokenTag : ITag
    {
        public OokTokenTypes type { get; private set; }

        public OokTokenTag(OokTokenTypes type)
        {
            this.type = type;
        }
    }

}
