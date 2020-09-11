using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalExpectionFilterDemo.Log
{
        public class LogHelper : ILoggerHelper
        {
                private readonly ConcurrentDictionary<Type , ILog> Loggers = new ConcurrentDictionary<Type , ILog>();

                private ILog GetLogger(Type source )
                {
                        if (Loggers.ContainsKey(source))
                        {
                                return Loggers [ source ];
                        }
                        else
                        {
                                ILog logger = LogManager.GetLogger(Startup.repository.Name , source);
                                Loggers.TryAdd(source , logger);
                                return logger;
                        }
                }


                public void Debug( object source , string message )
                {
                        Debug(source.GetType(), message) ;
                }

                public void Debug( object source , string message , params object [ ] ps )
                {
                        Debug(source.GetType(), string.Format(message,ps));
                }

                public void Debug( Type source , string message )
                {
                        ILog logger = GetLogger(source);
                        if ( logger.IsDebugEnabled )
                        {
                                logger.Debug(message);
                        }
                }

                public void Debug( object source , object message , Exception exception )
                {
                        Debug(source.GetType() , message , exception);
                }

                public void Debug( Type source , object message , Exception exception )
                {
                        GetLogger(source).Debug(message, exception);
                }

                public void Error( object source , object message )
                {
                        Error(source.GetType(), message);
                }

                public void Error( Type source , object message )
                {
                        ILog logger = GetLogger(source);
                        if (logger.IsErrorEnabled)
                        {
                                logger.Error(message);
                        }
                }

                public void Error( object source , object message , Exception exception )
                {
                        Error(source.GetType() , message , exception);
                }

                public void Error( Type source , object message , Exception exception )
                {
                        GetLogger(source).Error(message, exception);
                }

                public void Fatal( object source , object message )
                {
                        Fatal(source.GetType() , message);
                }

                public void Fatal( Type source , object message )
                {
                        ILog logger = GetLogger(source);
                        if (logger.IsFatalEnabled)
                        {
                                logger.Fatal(message);
                        }
                }

                public void Fatal( object source , object message , Exception exception )
                {
                        Fatal(source.GetType(), message, exception);
                }

                public void Fatal( Type source , object message , Exception exception )
                {
                        GetLogger(source).Fatal(message, exception);
                }

                public void Info( object source , object message )
                {
                        Info(source.GetType() , message);
                }

                public void Info( Type source , object message )
                {
                        ILog logger = GetLogger(source);
                        if (logger.IsInfoEnabled)
                        {
                                logger.Info(message);
                        }
                }

                public void Info( object source , object message , Exception exception )
                {
                        Info(source.GetType() , message , exception);
                }

                public void Info( Type source , object message , Exception exception )
                {
                        GetLogger(source).Info(message , exception);
                }

                public void Warn( object source , object message )
                {
                        Warn(source.GetType(), message);
                }

                public void Warn( Type source , object message )
                {
                        ILog logger = GetLogger(source);
                        if (logger.IsWarnEnabled)
                        {
                                logger.Warn(message);
                        }
                }

                public void Warn( object source , object message , Exception exception )
                {
                        Warn(source.GetType() , message , exception);
                }

                public void Warn( Type source , object message , Exception exception )
                {
                        GetLogger(source).Warn(message , exception);
                }
        }
}
