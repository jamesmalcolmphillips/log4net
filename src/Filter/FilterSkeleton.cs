#region Copyright & License
//
// Copyright 2001-2004 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using log4net.Core;

namespace log4net.Filter
{
	/// <summary>
	/// Users should extend this class to implement customized logging
	/// event filtering. 
	/// </summary>
	/// <remarks>
	/// <para>Users should extend this class to implement customized logging
	/// event filtering. Note that <see cref="log4net.Repository.Hierarchy.Logger"/> and 
	/// <see cref="log4net.Appender.AppenderSkeleton"/>, the parent class of all standard
	/// appenders, have built-in filtering rules. It is suggested that you
	/// first use and understand the built-in rules before rushing to write
	/// your own custom filters.</para>
	/// 
	/// <para>This abstract class assumes and also imposes that filters be
	/// organized in a linear chain. The <see cref="Decide"/>
	/// method of each filter is called sequentially, in the order of their 
	/// addition to the chain.</para>
	/// 
	/// <para>The <see cref="Decide"/> method must return one
	/// of the integer constants <see cref="FilterDecision.Deny"/>, <see cref="FilterDecision.Neutral"/> or <see cref="FilterDecision.Accept"/>.</para>
	/// 
	/// <para>If the value <see cref="FilterDecision.Deny"/> is returned, then the log event is dropped 
	/// immediately without consulting with the remaining filters. </para>
	/// 
	/// <para>If the value <see cref="FilterDecision.Neutral"/> is returned, then the next filter
	/// in the chain is consulted. If there are no more filters in the
	/// chain, then the log event is logged. Thus, in the presence of no
	/// filters, the default behavior is to log all logging events.</para>
	/// 
	/// <para>If the value <see cref="FilterDecision.Accept"/> is returned, then the log
	/// event is logged without consulting the remaining filters. </para>
	/// 
	/// <para>The philosophy of log4net filters is largely inspired from the
	/// Linux ipchains. </para>
	/// </remarks>
	/// <author>Nicko Cadell</author>
	/// <author>Gert Driesen</author>
	public abstract class FilterSkeleton : IFilter
	{
		#region Member Variables

		/// <summary>
		/// Points to the next filter in the filter chain.
		/// </summary>
		/// <remarks>
		/// See <see cref="Next"/> for more information.
		/// </remarks>
		private IFilter m_next;

		#endregion

		#region Implementation of IOptionHandler

		/// <summary>
		/// Usually filters options become active when set. 
		/// We provide a default do-nothing implementation for convenience.
		/// </summary>
		virtual public void ActivateOptions() 
		{
		}

		#endregion

		#region Implementation of IFilter

		/// <summary>
		/// Decide if the <see cref="LoggingEvent"/> should be logged through an appender.
		/// </summary>
		/// <param name="loggingEvent">The <see cref="LoggingEvent"/> to decide upon</param>
		/// <returns>The decision of the filter</returns>
		/// <remarks>
		/// <para>If the decision is <see cref="FilterDecision.Deny"/>, then the event will be
		/// dropped. If the decision is <see cref="FilterDecision.Neutral"/>, then the next
		/// filter, if any, will be invoked. If the decision is <see cref="FilterDecision.Accept"/> then
		/// the event will be logged without consulting with other filters in
		/// the chain.</para>
		/// 
		/// <para>This method is marked <c>abstract</c> and must be implemented
		/// in a subclass.</para>
		/// </remarks>
		abstract public FilterDecision Decide(LoggingEvent loggingEvent);

		/// <summary>
		/// Property to get and set the next filter in the filter
		/// chain of responsibility.
		/// </summary>
		/// <value>
		/// The next filter in the chain
		/// </value>
		/// <remarks>
		/// Filters are typically composed into chains. This property allows the next filter in 
		/// the chain to be accessed.
		/// </remarks>
		public IFilter Next
		{
			get { return m_next; }
			set { m_next = value; }
		}

		#endregion
	}
}