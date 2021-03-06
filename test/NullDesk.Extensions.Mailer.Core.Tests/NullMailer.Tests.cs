﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using NullDesk.Extensions.Mailer.Core.Fluent.Extensions;
using Xunit;

namespace NullDesk.Extensions.Mailer.Core.Tests
{
    public class NullMailerTests
    {
        private NullMailer GetMailer()
        {
            return new NullMailer(_mailerSettings);
        }

        private readonly NullMailerSettings _mailerSettings =
            new NullMailerSettings
            {
                FromEmailAddress = "nowhere@nowhere.com",
                FromDisplayName = "Mr. NoBody"
            };

        [Theory]
        [InlineData(null, "toast@toast.com", "something")]
        [InlineData("toast@toast.com", null, "something")]
        [InlineData("toast@toast.com", "toast@toast.com", null)]
        [Trait("TestType", "Unit")]
        public void NullMailer_AddMessage_FailsWhenNotDeliverable(string from, string to, string textBody)
        {
            var mailer = GetMailer();
            mailer.Invoking(m => m.AddMessage(
                    new MailerMessage
                    {
                        From = string.IsNullOrEmpty(from) ? null : new MessageSender {EmailAddress = from},
                        Recipients = string.IsNullOrEmpty(to)
                            ? new List<MessageRecipient>()
                            : new List<MessageRecipient>
                            {
                                new MessageRecipient
                                {
                                    EmailAddress = to
                                }
                            },
                        Body = string.IsNullOrEmpty(textBody) ? null : new ContentBody().WithPlainText(textBody)
                    }))
                .ShouldThrow<ArgumentException>();
        }

        [Fact]
        [Trait("TestType", "Unit")]
        public void NullMailer_AddMessage()
        {
            var mailer = GetMailer();
            mailer.AddMessage(new MailerMessage()
                .To("noone@nowhere.com")
                .From(_mailerSettings.FromEmailAddress, _mailerSettings.FromDisplayName)
                .WithSubject("Some Topic")
                .WithBody<ContentBody>(b => b.PlainTextContent = "something"));

            ((IMailer) mailer).PendingDeliverables
                .Should()
                .NotBeEmpty()
                .And.Contain(m => m.Subject == "Some Topic");
        }

        [Fact]
        [Trait("TestType", "Unit")]
        public void NullMailer_AddMessages()
        {
            var mailer = GetMailer();
            var ids = mailer.AddMessages(new List<MailerMessage>
            {
                new MailerMessage()
                    .To("noone@nowhere.com")
                    .From(_mailerSettings.FromEmailAddress, _mailerSettings.FromDisplayName)
                    .WithSubject("Some Topic")
                    .WithBody<ContentBody>(b => b.PlainTextContent = "something"),
                new MailerMessage()
                    .To("noone@nowhere.com")
                    .From(_mailerSettings.FromEmailAddress, _mailerSettings.FromDisplayName)
                    .WithSubject("Some Other Topic")
                    .WithBody<ContentBody>(b => b.PlainTextContent = "something")
            });

            ((IMailer) mailer).PendingDeliverables
                .Should()
                .HaveCount(2)
                .And.Contain(m => m.Subject == "Some Topic")
                .And.Contain(m => m.Subject == "Some Other Topic");
        }


        [Fact]
        [Trait("TestType", "Unit")]
        public void NullMailer_Create_FromIBuilderStepsCompleted()
        {
            var mailer = GetMailer();
            mailer.CreateMessage(b => b
                .Subject("Some Topic")
                .And.To("nooneelse@nowhere.com")
                .And.ForBody()
                .WithPlainText("body text"));

            ((IMailer) mailer).PendingDeliverables.Should().Contain(m => m.Subject == "Some Topic");
        }

        [Fact]
        [Trait("TestType", "Unit")]
        public void NullMailer_Create_MailerMessage()
        {
            var mailer = GetMailer();
            mailer.CreateMessage(b => b
                .Subject("Some Topic")
                .And.To("nooneelse@nowhere.com")
                .And.ForBody()
                .WithPlainText("body text")
                .Build());

            ((IMailer) mailer).PendingDeliverables
                .Should()
                .NotBeEmpty()
                .And.Contain(m => m.Subject == "Some Topic");
        }
    }
}