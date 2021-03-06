#region Apache Notice

/*****************************************************************************
 * $Revision: 387044 $
 * $LastChangedDate: 2006-03-19 23:02:00 +0100 (dim., 19 mars 2006) $
 * $LastChangedBy: gbayon $
 * 
 * iBATIS.NET Data Mapper
 * Copyright (C) 2006/2005 - The Apache Software Foundation
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

#region Using

using System;
using System.Xml.Serialization;

#endregion

namespace SqlBatis.DataMapper
{
    /// <summary>
    ///     Information about a data source.
    /// </summary>
    [Serializable]
    [XmlRoot("dataSource", Namespace = "http://ibatis.apache.org/dataMapper")]
    public class DataSource : IDataSource
    {
        #region Constructor (s) / Destructor

        #endregion

        #region Fields

        [NonSerialized] private string _connectionString = string.Empty;

        [NonSerialized] private IDbProvider _provider;

        [NonSerialized] private string _name = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        ///     The connection string.
        /// </summary>
        [XmlAttribute("connectionString")]
        public virtual string ConnectionString
        {
            get => _connectionString;
            set
            {
                CheckPropertyString("ConnectionString", value);
                _connectionString = value;
            }
        }

        /// <summary>
        ///     DataSource Name
        /// </summary>
        [XmlAttribute("name")]
        public string Name
        {
            get => _name;
            set
            {
                CheckPropertyString("Name", value);
                _name = value;
            }
        }

        /// <summary>
        ///     The provider to use for this data source.
        /// </summary>
        [XmlIgnore]
        public virtual IDbProvider DbProvider
        {
            get => _provider;
            set => _provider = value;
        }

        #endregion

        #region Methods

        private void CheckPropertyString(string propertyName, string value)
        {
            if (value == null || value.Trim().Length == 0)
                throw new ArgumentException(
                    "The " + propertyName + " property cannot be " +
                    "set to a null or empty string value.", propertyName);
        }

        /// <summary>
        ///     ToString implementation.
        /// </summary>
        /// <returns>A string that describes the data source</returns>
        public override string ToString()
        {
            return "Source: ConnectionString : " + ConnectionString;
        }

        #endregion
    }
}