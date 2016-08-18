//***************************************************************************
// 
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Visual Studio SDK license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

namespace OokLanguage.Tag
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Tagging;

    internal sealed class OokTokenTagger : ITagger<OokTokenTag>
    {

        ITextBuffer _buffer;
        IDictionary<string, OokTokenTypes> _ookTypes;

        internal OokTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
            _ookTypes = new Dictionary<string, OokTokenTypes>();
            _ookTypes["ook!"] = OokTokenTypes.OokExclamation;
            _ookTypes["ook."] = OokTokenTypes.OokPeriod;
            _ookTypes["ook?"] = OokTokenTypes.OokQuestion;



            _ookTypes["delete"] = OokTokenTypes.CakeFunction;
            _ookTypes["copy"] = OokTokenTypes.CakeFunction;
            _ookTypes["cd"] = OokTokenTypes.CakeFunction;
            _ookTypes["hello"] = OokTokenTypes.CakeFunction;

            /*
            CakeFunctions.Functions.ForEach(x => {
                _ookTypes[x] = OokTokenTypes.CakeFunction;
            });
            */
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<OokTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {

            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;
                string[] tokens = containingLine.GetText().ToLower().Split(' ');

                foreach (string ookToken in tokens)
                {
                    if (_ookTypes.ContainsKey(ookToken))
                    {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc, ookToken.Length));
                        if( tokenSpan.IntersectsWith(curSpan) ) 
                            yield return new TagSpan<OokTokenTag>(tokenSpan, 
                                                                  new OokTokenTag(_ookTypes[ookToken]));
                    }

                    //add an extra char location because of the space
                    curLoc += ookToken.Length + 1;
                }
            }
            
        }
    }
}
