using MoustakbalManager.Objects;
using System;
using System.Collections.Generic;

namespace MoustakbalManager.Tools.Managers
{
	public static class MemberManager
	{
		public static int GetMemberById(int ID, List<Member> members)
		{
			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].ID == ID)
				{
					return i;
				}
			}
			return -1;
		}

		/*public static void CopyMember(Member targetMember,Member sourceMember)
		{
			targetMember.Name = sourceMember.Name;
			targetMember.LastName = sourceMember.LastName;
			targetMember.BirthPlace.CopyAdressLocation(sourceMember.BirthPlace);
			targetMember.BirthDate = sourceMember.BirthDate;
			targetMember.studyLevel = sourceMember.studyLevel;
			targetMember.Fonction = sourceMember.Fonction;
			targetMember.EMail = sourceMember.EMail;
			targetMember.CurrentAdress.CopyAdressLocation(sourceMember.CurrentAdress);
			targetMember.PhoneNumber = sourceMember.PhoneNumber;
		}*/


	}
}
