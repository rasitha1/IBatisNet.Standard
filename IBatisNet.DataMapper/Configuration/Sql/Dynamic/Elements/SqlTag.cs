#region Apache Notice

/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
 * $Date: 2006-03-04 15:21:51 +0100 (sam., 04 mars 2006) $
 * 
 * iBATIS.NET Data Mapper
 * Copyright (C) 2004 - Gilles Bayon
 *  
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 ********************************************************************************/

#endregion

using System;
using System.Collections;
using System.Xml.Serialization;
using IBatisNet.DataMapper.Configuration.Sql.Dynamic.Handlers;

namespace IBatisNet.DataMapper.Configuration.Sql.Dynamic.Elements
{
    /// <summary>
    ///     SqlTag is a children element of dynamic Sql element.
    ///     SqlTag represent any binary unary/conditional element (like isEmpty, isNull, iEquall...)
    ///     or other element as isParameterPresent, isNotParameterPresent, iterate.
    /// </summary>
    [Serializable]
    public abstract class SqlTag : ISqlChild, IDynamicParent
    {
        /// <summary>
        ///     Parent tag element
        /// </summary>
        [XmlIgnore]
        public SqlTag Parent
        {
            get => _parent;
            set => _parent = value;
        }


        /// <summary>
        ///     Prepend attribute
        /// </summary>
        [XmlAttribute("prepend")]
        public string Prepend
        {
            get => _prepend;
            set => _prepend = value;
        }


        /// <summary>
        ///     Handler for this sql tag
        /// </summary>
        [XmlIgnore]
        public ISqlTagHandler Handler
        {
            get => _handler;
            set => _handler = value;
        }

        /// <summary>
        /// </summary>
        public bool IsPrependAvailable => (_prepend != null && _prepend.Length > 0);

        #region IDynamicParent Members

        /// <summary>
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(ISqlChild child)
        {
            if (child is SqlTag) ((SqlTag) child).Parent = this;
            _children.Add(child);
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetChildrenEnumerator()
        {
            return _children.GetEnumerator();
        }

        #region Fields

        [NonSerialized] private string _prepend = string.Empty;

        [NonSerialized] private ISqlTagHandler _handler;

        [NonSerialized] private SqlTag _parent;

        [NonSerialized] private readonly IList _children = new ArrayList();

        #endregion
    }
}