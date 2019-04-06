using UnityEngine.UI;
using DG.Tweening;

public static class TextExtention
{
	/*
	 * t.CountUp(100);
	 */
	public static void CountUp(this Text self, int after)
	{
		int before = int.Parse(self.text);
		DOTween.To(
			() => before,
			(i) =>
			{
				self.text = i.ToString();
			},
			after,
			1f
		);
	}
}

