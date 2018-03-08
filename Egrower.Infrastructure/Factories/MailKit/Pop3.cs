using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Pop3;
using MailKit.Security;

namespace Egrower.Infrastructure.Factories.MailKit {
    public static class Pop3 {
        #region ProtocolLogger
        public static void DownloadMessages () {
            using (var client = new Pop3Client (new ProtocolLogger ("pop3.log"))) {
                client.Connect ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

                //test acount on gmail
                client.Authenticate ("ehodowcatest@gmail.com", "ehodowca");

                for (int i = 0; i < client.Count; i++) {
                    var message = client.GetMessage (i);

                    string messageStr = string.Format ("MAIL: body : {0} From: {1} , To : {2}  , Date : {3} , Sender: {4}, Subject: {5}, Text Body: {6} ", message.Body, message.From, message.To, message.Date, message.Sender, message.Subject, message.TextBody);

                    string messageDatas = string.Format ("MAILs datas:  Attachments: {0} ResentFrom: {1} , Cc : {2}  , Headers : {3} , Importance: {4}, References: {5}, ResentBcc: {6} , ResentCc: {7} ,  ReplyTo: {8} , ResentFrom: {9} ,  ResentSender: {10}  ,  ResentTo: {11}, XPriority {12} ", message.Attachments, message.ResentFrom, message.Cc, message.Headers, message.Importance, message.References, message.ResentBcc, message.ResentCc, message.ReplyTo, message.ResentFrom, message.ResentSender, message.ResentTo, message.XPriority);
                    System.Console.WriteLine ("MAIL :");
                    System.Console.WriteLine (messageStr);
                    System.Console.WriteLine ("-------------------------");
                    System.Console.WriteLine ("DATAS:  :");
                    System.Console.WriteLine ("Działa");
                    System.Console.WriteLine (messageDatas);
                    // write the message to a file
                    message.WriteTo (string.Format ("{0}.msg", i));

                    // mark the message for deletion
                    client.DeleteMessage (i);
                }

                client.Disconnect (true);
            }
        }
        public static  List<string> GetDownloadMessages () {
            List<string> messages = new List<string>();
            List<string> messageDatas = new List<string>();
            using (var client = new Pop3Client (new ProtocolLogger ("pop3.log"))) {
                client.Connect ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

                //test acount on gmail
                client.Authenticate ("dragonet.c17@gmail.com", "assasin17");
                System.Console.WriteLine(client. IsAuthenticated);
                for (int i = 0; i < client.Count; i++) {
                    var message = client.GetMessage (i);

                    string a = string.Format ("MAIL: body : {0} From: {1} , To : {2}  , Date : {3} , Sender: {4}, Subject: {5}, Text Body: {6} ", message.Body, message.From, message.To, message.Date, message.Sender, message.Subject, message.TextBody);
                    messages.Add(a);
                    string b = string.Format ("MAILs datas:  Attachments: {0} ResentFrom: {1} , Cc : {2}  , Headers : {3} , Importance: {4}, References: {5}, ResentBcc: {6} , ResentCc: {7} ,  ReplyTo: {8} , ResentFrom: {9} ,  ResentSender: {10}  ,  ResentTo: {11}, XPriority {12} ", message.Attachments, message.ResentFrom, message.Cc, message.Headers, message.Importance, message.References, message.ResentBcc, message.ResentCc, message.ReplyTo, message.ResentFrom, message.ResentSender, message.ResentTo, message.XPriority);
                    System.Console.WriteLine ("MAIL :");
                    messageDatas.Add(b);
                    System.Console.WriteLine (a);
                    System.Console.WriteLine ("-------------------------");
                    System.Console.WriteLine ("DATAS:  :");
                    System.Console.WriteLine ("Działa");
                    System.Console.WriteLine (b);
                    // write the message to a file
                    message.WriteToAsync (string.Format (@"..\\Egrower.Infrastructure\\MsgFiles\\Dragonet\\"+"dragonet{0}.msg", i));

                    // mark the message for deletion
                    client.DeleteMessage (i);
                }

                client.Disconnect (true);
            }
             return messages;
            //  return messageDatas;
        }
        #endregion

        #region Capabilities
        public static void PrintCapabilities () {
            using (var client = new Pop3Client ()) {
                client.Connect ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

                if (client.Capabilities.HasFlag (Pop3Capabilities.Sasl)) {
                    var mechanisms = string.Join (", ", client.AuthenticationMechanisms);
                    Console.WriteLine ("The POP3 server supports the following SASL mechanisms: {0}", mechanisms);
                }

                client.Authenticate ("username", "password");

                if (client.Capabilities.HasFlag (Pop3Capabilities.Apop))
                    Console.WriteLine ("The server supports APOP authentication.");

                if (client.Capabilities.HasFlag (Pop3Capabilities.Expire)) {
                    if (client.ExpirePolicy > 0)
                        Console.WriteLine ("The POP3 server automatically expires messages after {0} days", client.ExpirePolicy);
                    else
                        Console.WriteLine ("The POP3 server will never expire messages.");
                }

                if (client.Capabilities.HasFlag (Pop3Capabilities.LoginDelay))
                    Console.WriteLine ("The minimum number of seconds between login attempts is {0}.", client.LoginDelay);

                if (client.Capabilities.HasFlag (Pop3Capabilities.Pipelining))
                    Console.WriteLine ("The POP3 server can pipeline commands, so using client.GetMessages() will be faster.");

                if (client.Capabilities.HasFlag (Pop3Capabilities.Top))
                    Console.WriteLine ("The POP3 server supports the TOP command, so it's possible to download message headers.");

                if (client.Capabilities.HasFlag (Pop3Capabilities.UIDL))
                    Console.WriteLine ("The POP3 server supports the UIDL command which means we can track messages by UID.");

                client.Disconnect (true);
            }
        }
        #endregion

        #region DownloadMessages
        public static void DownloadMessages1 () {
            using (var client = new Pop3Client ()) {
                client.Connect ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

                client.Authenticate ("username", "password");

                for (int i = 0; i < client.Count; i++) {
                    var message = client.GetMessage (i);

                    // write the message to a file
                    message.WriteTo (string.Format ("{0}.msg", i));

                    // mark the message for deletion
                    client.DeleteMessage (i);
                }

                client.Disconnect (true);

                Console.WriteLine (client);
            }

        }
        #endregion

        //#region BatchDownloadMessages
        //public static void DownloadMessages2()
        //{
        //    using (var client = new Pop3Client())
        //    {
        //        client.Connect("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

        //        client.Authenticate("username", "password");

        //        var messages = client.GetMessage(0, count)//(0, count);

        //        foreach (var message in messages)
        //        {
        //            // write the message to a file
        //            message.WriteTo(string.Format("{0}.msg", i));
        //        }

        //        client.DeleteMessages(0, count);

        //        client.Disconnect(true);
        //    }
        //}
        //#endregion

        #region DownloadNewMessages
        public static void DownloadNewMessages3 (HashSet<string> previouslyDownloadedUids) {
            using (var client = new Pop3Client ()) {
                client.Connect ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

                client.Authenticate ("username", "password");

                if (!client.Capabilities.HasFlag (Pop3Capabilities.UIDL))
                    throw new Exception ("The POP3 server does not support UIDs!");

                var uids = client.GetMessageUids ();

                for (int i = 0; i < client.Count; i++) {
                    // check that we haven't already downloaded this message
                    // in a previous session
                    if (previouslyDownloadedUids.Contains (uids[i]))
                        continue;

                    var message = client.GetMessage (i);

                    // write the message to a file
                    message.WriteTo (string.Format ("{0}.msg", uids[i]));

                    // add the message uid to our list of downloaded uids
                    previouslyDownloadedUids.Add (uids[i]);
                }

                client.Disconnect (true);
            }
        }
        #endregion

        #region ExceptionHandling
        public static void DownloadNewMessages (HashSet<string> previouslyDownloadedUids) {
            using (var client = new Pop3Client ()) {
                IList<string> uids = null;

                try {
                    client.Connect ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);
                } catch (Pop3CommandException ex) {
                    Console.WriteLine ("Error trying to connect: {0}", ex.Message);
                    Console.WriteLine ("\tStatusText: {0}", ex.StatusText);
                    return;
                } catch (Pop3ProtocolException ex) {
                    Console.WriteLine ("Protocol error while trying to connect: {0}", ex.Message);
                    return;
                }

                try {
                    client.Authenticate ("username", "password");
                } catch (AuthenticationException ex) {
                    Console.WriteLine ("Invalid user name or password.");
                    return;
                } catch (Pop3CommandException ex) {
                    Console.WriteLine ("Error trying to authenticate: {0}", ex.Message);
                    Console.WriteLine ("\tStatusText: {0}", ex.StatusText);
                    return;
                } catch (Pop3ProtocolException ex) {
                    Console.WriteLine ("Protocol error while trying to authenticate: {0}", ex.Message);
                    return;
                }

                // for the sake of this example, let's assume GMail supports the UIDL extension
                if (client.Capabilities.HasFlag (Pop3Capabilities.UIDL)) {
                    try {
                        uids = client.GetMessageUids ();
                    } catch (Pop3CommandException ex) {
                        Console.WriteLine ("Error trying to get the list of uids: {0}", ex.Message);
                        Console.WriteLine ("\tStatusText: {0}", ex.StatusText);

                        // we'll continue on leaving uids set to null...
                    } catch (Pop3ProtocolException ex) {
                        Console.WriteLine ("Protocol error while trying to authenticate: {0}", ex.Message);

                        // Pop3ProtocolExceptions often cause the connection to drop
                        if (!client.IsConnected)
                            return;
                    }
                }

                for (int i = 0; i < client.Count; i++) {
                    if (uids != null && previouslyDownloadedUids.Contains (uids[i])) {
                        // we must have downloaded this message in a previous session
                        continue;
                    }

                    try {
                        // download the message at the specified index
                        var message = client.GetMessage (i);

                        // write the message to a file
                        if (uids != null) {
                            message.WriteTo (string.Format ("{0}.msg", uids[i]));

                            // keep track of our downloaded message uids so we can skip downloading them next time
                            previouslyDownloadedUids.Add (uids[i]);
                        } else {
                            message.WriteTo (string.Format ("{0}.msg", i));
                        }
                    } catch (Pop3CommandException ex) {
                        Console.WriteLine ("Error downloading message {0}: {1}", i, ex.Message);
                        Console.WriteLine ("\tStatusText: {0}", ex.StatusText);
                        continue;
                    } catch (Pop3ProtocolException ex) {
                        Console.WriteLine ("Protocol error while sending message {0}: {1}", i, ex.Message);
                        // most likely the connection has been dropped
                        if (!client.IsConnected)
                            break;
                    }
                }

                if (client.IsConnected) {
                    // if we do not disconnect cleanly, then the messages won't actually get deleted
                    client.Disconnect (true);
                }
            }
        }
        #endregion
    }
}