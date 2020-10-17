using System;
using Meta.SQL;

namespace Ps2Css.DB.Request
{
	public class Module : Meta.Framework.Module<Item>
	{
		public Module(IDatabase db) : base(db)
		{
		}

		public Item GetByID(Guid id)
		{
			ICommand cmd = this.db.StoredProcedure("spRequest#SelectByID");
			cmd.Parameters.Add("ID", id);

			return cmd.ExecuteRecord(dr => new Item(dr));
		}
	}
}
