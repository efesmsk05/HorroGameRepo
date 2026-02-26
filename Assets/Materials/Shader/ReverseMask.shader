Shader "Custom/ReversMask"
{

	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)

		_MainText("Base (RGB) Gloss (A)" , 2D) = "White"{}
		}


	Category
	{
		SubShader
		{

			Tags{"Queue" = "Transparent+1"}

			Pass
			{
				ZWrite On
				ZTest Greater
				Lighting On	
				SetTexture [_MainText]{}


			}

		}

		FallBack "Specular" , 1


	}

}