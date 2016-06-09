using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace StridersVR.Domain.SpeedPack
{
	public class Suitcase 
	{
		private List<SuitcasePart> suitcasePartList;

		private int suitcaseScore;

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

		public void addSuitcasePartMain(SuitcasePart newPart)
		{
			if (this.suitcasePartList.Count == 0) 
			{
				this.suitcasePartList.Add (newPart);
				this.setMainPart();
			}
			else if (this.suitcasePartList.Count > 0) 
			{
				newPart.setAttachedPart(this.getMainPart());
				this.suitcasePartList.Add (newPart);
			}
		}

		public void setMainPart()
		{
			if (this.suitcasePartList.Count > 0) 
			{
				this.suitcasePartList[0].setMainPart();
			}
		}

		public void setSuicaseScore(int score)
		{
			this.suitcaseScore = score;
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

		public int SuitcaseScore
		{
			get { return this.suitcaseScore; }
		}
		#endregion

	}
}
