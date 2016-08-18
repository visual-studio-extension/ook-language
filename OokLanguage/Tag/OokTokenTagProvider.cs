using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OokLanguage.Tag
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("ook!")]
    [TagType(typeof(OokTokenTag))]
    internal sealed class OokTokenTagProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new OokTokenTagger(buffer) as ITagger<T>;
        }
    }

}
