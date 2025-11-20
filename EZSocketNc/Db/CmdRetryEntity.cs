
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZSocketNc.Db
{
    [Table("local_cmd_storage")]
    public class CmdRetryEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("key")]
        public string Key { get; set; }

        [Column("data_json")]
        public string DataJson { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("retry_times")]
        public int RetryTimes { get; set; }
    }
}
