﻿using System;

namespace Demo.Persistence.Core
{
    public class ReminderType
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
    }
}
