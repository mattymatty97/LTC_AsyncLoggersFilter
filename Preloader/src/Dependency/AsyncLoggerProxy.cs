//
// PROXY CLASS FOR SOFT DEPENDING ON AsyncLoggers
//
// This is free and unencumbered software released into the public domain.
//
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
//
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// For more information, please refer to <https://unlicense.org>

using System;
using System.Runtime.CompilerServices;
using AsyncLoggers.DBAPI;
using BepInEx.Logging;

namespace AsyncLoggers.Filter.Preloader.Dependency
{
    public static class AsyncLoggerProxy
    { 
        private static bool? _installed;
        public static bool Installed
        {
            get
            {
                if (_installed.HasValue)
                    return _installed.Value;
                try
                {
                    IsDbEnabled();
                    _installed = true;
                }catch (Exception)            
                {                
                    _installed = false;
                    return false;
                }
                return _installed.Value;
            }
        }
        
        private static bool? _enabled;
        public static bool Enabled
        {
            get
            {
                if (_enabled.HasValue)
                    return _enabled.Value;
                try
                {
                    _enabled = IsDbEnabled();
                }catch (Exception)            
                {                
                    _enabled = false;
                    return false;
                }
                return _enabled.Value;
            }
        }
    
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void WriteEvent(string source, string tag, string data, DateTime? timestamp = null)
        {
            SqliteLogger.WriteEvent(source, tag, data, timestamp);
        }
    
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void WriteData(string source, string tag, string data, DateTime? timestamp = null)
        {
            SqliteLogger.WriteData(source, tag, data, timestamp);
        }
    
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static bool IsDbEnabled()
        {
            return SqliteLogger.Enabled;
        }
    
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static int GetExecutionID()
        {
            return SqliteLogger.ExecutionId;
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static ILogSource GetLogger()
        {
            return AsyncLoggers.Log;
        }
    
    }
}
