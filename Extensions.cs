/*
Copyright © Stephen Kennedy 2019  

This file is part of Thinkpad-Backlight.

Thinkpad-Backlight is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Thinkpad-Backlight is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Thinkpad-Backlight.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Linq;
using System.Reflection;

namespace Thinkpad_Backlight
{
    public static class Extensions
    {
        public static void Reset(this System.Windows.Forms.Timer timer)
        {
            if (timer == null)
                throw new ArgumentNullException(nameof(timer));

            timer.Stop();
            timer.Start();
        }

        public static MethodInfo GetRuntimeMethodsExt(this Type type, string name, params Type[] types)
        {
            // https://stackoverflow.com/a/21308619/397817
            // Find potential methods with the correct name and the right number of parameters
            // and parameter names
            var potentials = (from ele in type.GetMethods()
                where ele.Name.Equals(name)
                //let param = ele.GetParameters()
                //where param.Length == types.Length
                //&& param.Select(p => p.ParameterType.Name).SequenceEqual(types.Select(t => t.Name))
                select ele);

            // Maybe check if we have more than 1? Or not?
            return potentials.FirstOrDefault();
        }
    }
}