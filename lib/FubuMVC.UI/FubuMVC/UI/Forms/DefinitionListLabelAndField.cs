// Type: FubuMVC.UI.Forms.DefinitionListLabelAndField
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System.Collections.Generic;
using HtmlTags;

namespace FubuMVC.UI.Forms
{
    public class DefinitionListLabelAndField : ILabelAndFieldLayout, ITagSource
    {
        private readonly HtmlTag _dd = new HtmlTag("dd");
        private readonly HtmlTag _dt = new HtmlTag("dt");
        private HtmlTag _bodyHolder;

        public DefinitionListLabelAndField()
        {
            _bodyHolder = _dd;
        }

        public HtmlTag DtTag
        {
            get { return _dt; }
        }

        public HtmlTag DdTag
        {
            get { return _dd; }
        }

        #region ILabelAndFieldLayout Members

        public HtmlTag LabelTag
        {
            get { return _dt.FirstChild(); }
            set
            {
                _dt.ReplaceChildren(new HtmlTag[1]
                                        {
                                            value
                                        });
            }
        }

        public HtmlTag BodyTag
        {
            get { return _bodyHolder.FirstChild(); }
            set
            {
                _bodyHolder.ReplaceChildren(new HtmlTag[1]
                                                {
                                                    value
                                                });
            }
        }

        public void WrapBody(HtmlTag tag)
        {
            tag.ReplaceChildren(new HtmlTag[1]
                                    {
                                        BodyTag
                                    });
            _bodyHolder.ReplaceChildren(new HtmlTag[1]
                                            {
                                                tag
                                            });
            _bodyHolder = tag;
        }

        public HtmlTag WrapBody(string tagName)
        {
            var tag = new HtmlTag(tagName);
            WrapBody(tag);
            return tag;
        }

        public void SetLabelText(string text)
        {
            _dt.Children.Clear();
            _dt.Text(text);
        }

        public IEnumerable<HtmlTag> AllTags()
        {
            yield return _dt;
            yield return _bodyHolder;
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}\n{1}", _dt, _bodyHolder);
        }
    }
}