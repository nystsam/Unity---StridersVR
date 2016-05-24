using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace StridersVR.Domain.SpeedPack
{
	public class Suitcase 
	{
		private List<SuitcasePart> suitcasePartList;

		public Suitcase()
		{
			this.suitcasePartList = new List<SuitcasePart> ();
		}


		public void addSuitcasePart(SuitcasePart newPart)
		{
			if (this.suitcasePartList.Count > 0) 
			{
				newPart.setAttachedPart(this.suitcasePartList.Last());
			}

			this.suitcasePartList.Add (newPart);
		}

		public void setMainPart()
		{
			if (this.suitcasePartList.Count > 0) 
			{
				this.suitcasePartList[0].setMainPart();
//				int _randomIndex = Random.Range(0, this.suitcasePartList.Count);
//
//				this.suitcasePartList[_randomIndex].setMainPart();
			}
		}

		public SuitcasePart getMainPart()
		{
			foreach (SuitcasePart part in this.suitcasePartList) 
			{
				if(part.IsMainPart)
				{
					return part;
				}
			}

			return null;
		}

		public SuitcasePart getPartAtIndex(int index)
		{
			return this.suitcasePartList [index];
		}

		#region Properties
		public List<SuitcasePart> SuitcasePartList 
		{
			get { return this.suitcasePartList; }
		}
		#endregion

	}
}
