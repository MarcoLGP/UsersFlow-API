﻿namespace UsersFlow_API.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string email, string subject, string content);
    }
}
