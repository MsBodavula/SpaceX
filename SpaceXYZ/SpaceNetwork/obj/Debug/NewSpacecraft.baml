   M S B A M L   `   `   `  ����G  CSpaceNetwork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SpaceNetworkSpaceNetwork  5   	        SpaceNetwork.NewSpacecraft   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SpaceNetwork     5   	     � Title$  NewSpacecraft��5   	   $	��450��6   $	��800��6,   .��5	      � $��0,0,1,0q�6   ���	#FFFFCC80 � ColumnDefinitions 5
   
   �� 5      $
��289* �6   �� 5      $	��50* �6   �� 5      $
��158* �6   �� 5      $
��294* �6   .+�5   
   �� $�� ��6   $
��Left=�6   $	��Top=�66   $��275,-13,0,0q�6N   �� 5   
   -   5         ��Name$ btnBack��5   $   $
��Bold"�6   $
��Back��65   $
��Left=�6D   $��12,9,0,0q�6_   $	��Top=�6q   $��75��6�   6�   �� 5   
   $��16(�6   $
��Bold"�6   $��Spacecraft information��60   $
��Left=�6Q   $��
268,33,0,0q�6l   $	��Top=�6�   $��3��6�   �� 5   
   -   5         ��Name$ txtSpacecraftName��5      $
��Left=�6-   $��23��6H   $��
300,77,0,0q�6T    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $	��145��6�   $��3��6�   $
��Bold"�6�   �� 5   
   $
��Bold"�6   $
��Name��6"   $��228,77,12,0q�61   $	��Top=�6F    J� RenderTransformOrigin$ 	0.76,1.04&�6^   �� 5   
   -   5        $ txtOrbitRadius��5      $
��Left=�6+   $��23��6F   $��300,113,0,0q�6R   $
 Wrap=�6g   $	��Top=�6{   $	��145��6�   $��3��6�   $
��Bold"�6�   �� 5   
   $
��Bold"�6   $��Orbit Radius��6"   $
��Left=�69   $��190,112,0,0q�6T   $	��Top=�6i   �� 5   
   $��16(�6   $
��Bold"�6   $��Payload Information��60   $
��Left=�6N   $��274,154,0,0q�6i   $	��Top=�6~   $��3��6�   �� 5   
   $
��Bold"�6   $
��Name��6"   $
��Left=�61   $��227,196,0,0q�6L   $	��Top=�6a   �� 5   
   $
��Bold"�6   $
��Type��6#   $
��Left=�62   $��233,231,0,0q�6M   $	��Top=�6b   �� 5   
   -   5        $ txtPayloadName��5   w   $��3��6   $
��Left=�6'   $��23��6B   $��300,191,0,0q�6N   $
 Wrap=�6c   $	��Top=�6�   $	��142��6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ comboPayload��5   {   $��3��6   $
��Left=�6'   $��300,231,0,0q�6B   $	��Top=�6W   $	��141��6o   $��25��6�   $
��Bold"�6�   �� 5   
   -   5        $ btnAddSpacecraft��5   #   $
��Bold"�6   $��3��6=   $��Add New Spacecraft��6Q   $
��Left=�6n   $��300,279,0,0q�6�   $	��Top=�6�   $	��141��6�   $��32��6�   5      5      