// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Proximity" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {} // Regular object texture 
        _PlayerPosition ("Player Position", vector) = (0,0,0,0) // The location of the player - will be set by script
        _VisibleDistance ("Visibility Distance", float) = 10.0 // How close does the player have to be to make object visible
        _OutlineWidth ("Outline Width", float) = 0.1 // Used to add an outline around visible area a la Mario Galaxy
        _OutlineColour ("Outline Colour", color) = (1.0,1.0,1.0,1.0) // Colour of the outline
        _EdgeBorder ("Edge Border", float) = 0.005
        _MaxDistance ("Max Distance", float) = 10.0
        _EnemyVisibleDistance ("Enemy Distance", float) = 0.0 //Distance to the enemy
        _EnemyPosition ("Enemy Position", vector) = (2,0,0,0)
       
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Pass {
        Blend SrcAlpha OneMinusSrcAlpha
         
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag       
 
        // Access the shaderlab properties
        uniform sampler2D _MainTex;
        uniform float4 _PlayerPosition;
        uniform float4 _EnemyPosition;
        uniform float _EnemyVisibleDistance;

        uniform float _VisibleDistance;
        uniform float _OutlineWidth;
        uniform fixed4 _OutlineColour;
        uniform float _EdgeBorder;
        uniform float _MaxDistance;
      
         
        // Input to vertex shader
        struct vertexInput {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
        };
        // Input to fragment shader
        struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 position_in_world_space : TEXCOORD0;
            float4 tex : TEXCOORD1;
        };
          
        // VERTEX SHADER
        vertexOutput vert(vertexInput input) 
        {
            vertexOutput output; 
            output.pos =  mul(UNITY_MATRIX_MVP, input.vertex);
            output.position_in_world_space = mul(unity_ObjectToWorld, input.vertex);
            output.tex = input.texcoord;
            return output;
        }
  
      
        float4 calculatePlayerRing(vertexOutput input)
        {
	         float dist = distance(input.position_in_world_space, _PlayerPosition);
	  			
	  		
	  		
	            // Return appropriate colour
	            if (dist < _VisibleDistance && dist < _MaxDistance) {
	            	//Edge shading only
	            	half inEdge = 0;
	            	half speedScale = 0.5;
	            	if(min(input.tex.x, input.tex.y) < _EdgeBorder || max(input.tex.x, input.tex.y) > (1 - _EdgeBorder))
	            	{
	            		inEdge = 0.8;
	            	}
	            	inEdge = (inEdge)/((_VisibleDistance - dist)*speedScale) - 0.1;

	                return float4(inEdge,inEdge,inEdge,1); // Visible
	            }
	            else if (dist < _VisibleDistance + _OutlineWidth && dist < _MaxDistance) {
	                return _OutlineColour; // Edge of visible range
	            }
	            else {
	                float4 tex = float4(0,0,0,1);//tex2D(_MainTex, float2(input.tex)); // Outside visible range
	                //tex.a = 0;
	                return tex;
	            }
        }

        float4 calculateEnemyRing(vertexOutput input)
        {
        	 // Return appropriate colour
        	  float dist = distance(input.position_in_world_space, _EnemyPosition);
	  		
	            if (dist < _EnemyVisibleDistance) {
	            	//Edge shading only
	            	half inEdge = 0;
	            	half speedScale = 0.5;
	            	if(min(input.tex.x, input.tex.y) < _EdgeBorder*3 || max(input.tex.x, input.tex.y) > (1 - _EdgeBorder) * 3)
	            	{
	            		inEdge = 0.8;
	            	}
	            	inEdge = (inEdge)/((_EnemyVisibleDistance - dist)*speedScale) - 0.1;
	            	inEdge *= tex2D(_MainTex,input.tex + float2(_EnemyVisibleDistance + dist,-_EnemyVisibleDistance + dist));
	                return float4(inEdge,inEdge*0.2,inEdge*0.2,1); // Visible
	            }
	            else if (dist < _EnemyVisibleDistance + _OutlineWidth/2) {
	                return float4(0.8,0.2,0.2,1); // Edge of visible range
	            }
	            else {
	                float4 tex = float4(0,0,0,1);//tex2D(_MainTex, float2(input.tex)); // Outside visible range
	                //tex.a = 0;
	                return tex;
	            }
        }
          // FRAGMENT SHADER
        float4 frag(vertexOutput input) : COLOR 
        {
            float4 playerRing = calculatePlayerRing(input);
            float4 enemyRing = calculateEnemyRing(input);
            return playerRing + enemyRing;
        }

 
        ENDCG
        } // End Pass
    } // End Subshader
    FallBack "Diffuse"
} // End Shader