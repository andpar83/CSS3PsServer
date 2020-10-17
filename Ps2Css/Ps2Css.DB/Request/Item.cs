using System;
using System.Data;
using Meta.Framework.Attribute;

namespace Ps2Css.DB.Request
{
	[Table("Request")][Serializable]
	public class Item : Meta.Framework.Item
	{
		internal Item(IDataRecord dr) : base(dr)
		{
		}
		public Item(Guid id)
		{
			this.ID = id;
		}

		[Column("ID")][PrimaryKey]
		public Guid ID
		{
			get
			{
				return this.id;
			}
			private set
			{
				this.id = value;
			}
		}
		private Guid id = Guid.Empty;

		[Column("ClientID")]
		public Guid ClientID
		{
			get
			{
				return this.clientID;
			}
			private set
			{
				this.clientID = value;
			}
		}
		private Guid clientID = Guid.Empty;

		[Column("IP")]
		public byte[] IP
		{
			get
			{
				return this.ip;
			}
			set
			{
				this.ip = value;
			}
		}
		private byte[] ip = null;

		[Column("Data")]
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}
		private byte[] data = new byte[0];

		[Column("Created")]
		public DateTime Created
		{
			get
			{
				return this.created;
			}
			set
			{
				this.created = value;
			}
		}
		private DateTime created = DateTime.Now;
	}
}
