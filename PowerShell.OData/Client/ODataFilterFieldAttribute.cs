﻿//--------------------------------------------------------------------------
//  <copyright file="ODataFilterFieldAttribute.cs" company="Microsoft">
//      Copyright (c) 2015 Microsoft Corporation.
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
//      furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//      THE SOFTWARE.
//  </copyright>
//--------------------------------------------------------------------------

namespace Microsoft.Dynamics.Marketing.Powershell.OData.Client
{
    using System;
    using System.CodeDom;

    /// <summary>
    /// Attribute that indicates that a property is used as a filter condition
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ODataFilterFieldAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the query parameter.
        /// </summary>
        public string Name
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the type of the query parameter.
        /// </summary>
        public Type FieldType
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        public ODataOperators.FilterOperatorType Operator
        {
            get; 
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataFilterFieldAttribute"/> class.
        /// </summary>
        public ODataFilterFieldAttribute()
        {
            this.FieldType = typeof(string);
            this.Operator = ODataOperators.FilterOperatorType.Equals;
        }
    }
}
