   M S B A M L   `   `   `  ����G  CSpaceNetwork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SpaceNetworkSpaceNetwork  5   	   "    SpaceNetwork.ViewSpacecrafts   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SpaceNetwork     5   	   -   5          � Title$  ViewSpacecrafts��5   	   $	��450��6!   $	��800��6.   .��5	      � $��0,0,17,0q�6   ���	#FFFFCC80.+�5
   
   �� -   5         ��Name$ WelcomeLabel��5
      $�� ��6&   $
��Left=�61   $��	189,3,0,0q�6L   $	��Top=�6_   $��15(�6w   $
��Bold"�6�    J� RenderTransformOrigin$ 
1.3,-0.467&�6�   $��26��6�   $	��158��6�   �� 5   
   -   5         ��Name$ spacecraftConnectedList��5      $
��Left=�64   $	��337��6O   ���	#FFBBE8F1$��	18,57,0,0q�6s   $	��Top=�6�   $	��216��6�   $��16(�6�   $
��Bold"�6�   �� 5   
   $��ACTIVE SPACECRAFTS��6   $
��Left=�6-   $��	43,26,0,0q�6H   $	��Top=�6[   $
��Bold"�6s   ���	#FF8B2727$	��181��6�   $��14(�6�   �� 5   
   -   5        $
��Back��5      $
��Left=�6    $��4,3,0,0q�6;   $	��Top=�6L   $��52��6d   $��21��6o   6{   �� 5   
   -   5         ��Name$ comboBoxSpacecrafts��5      $
��Left=�64   $��
258,56,0,0q�5   
   $	��Top=�6    $	��118��6:   $��27��5      $��16(�6   $
��Bold"�6`   �� 5   
   -   5        $ SelectSpacecraft��5      $��SELECT ACTIVE SPACECRAFT��6*   $
��Left=�6M   $��
253,26,0,0q�6h   $	��Top=�6|   $
��Bold"�6�   ���	#FF8B2727$	��202��6�   $��14(�6�   $��30��6�   �� 5   
   -   5         ��Name$ ButtonTelemetry��5   $   $
��Bold"�6   $��Request Telemetry��6>   $
��Left=�6Z   $��
260,99,0,0q�6u   $	��Top=�6�   $	��112��6�   $��25��6�   6�   �� 5   
   -   5         ��Name$ TextTelemetry��5   $   $
��Bold"�6   $
��Left=�6;   $	��229��6V   $��262,161,0,0q�6c    �� TextWrapping$
 Wrap=�6x   $y�Visible=�6�   $	��Top=�6�   $	��237��6�   $ 0.511,1.392&�6�   ���	#FFDF5A7C6�   �� 5   
   -	   5        $ LabelTelemetry��5      $��Random��6)   $y�Visible=�6:   $
��Left=�6P   $��260,133,0,0q�6k   $	��Top=�6�   $	��149��6�   $
��Bold"�6�   �� 5   
   -
   5        $ ButtonStopTelemetry��5   $   $
��Bold"�6   $��Stop Telemetry��6A   $
��Left=�6Z   $��
387,98,0,0q�6u   $y�Visible=�6�   $	��Top=�6�   $	��113��6�   $��26��6�   6�   �� 5   
   -   5        $ 
btnDeorbit��5   $   $
��Bold"�6   $��Deorbit Spacecraft��68   $
��Left=�6U   $��
385,55,0,0q�6p   $	��Top=�6�   $	��116��6�   $��26��6�   6�   �� 5   
   -   5        $ txtSpacecraftData��5   $   $
��Bold"�6   $
��Left=�6?   $	��227��6Z   $��576,161,0,0q�6g   $
 Wrap=�6|   $	��Top=�6�   $	��177��6�   $ 0.5,0.5&�6�   ���	#FFDF5A7C.��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.205Q�6&   T� 5      5      .��5   '   5       �� 5!   
   -   5        $
��Bold"�5!      $��Show Spacecraft Data��6$   $
��Left=�6C   $��
577,94,0,0q�6^   $	��Top=�6r   $	��177��6�   $��26��6�   6�   �� 5"   
   $��Spacecraft Data��6   $
��Left=�6*   $��571,130,0,0q�6E   $	��Top=�6Z   $
��Bold"�6r   $	��104��6�   �� 5#   
   -   5        $ comboForData��5#      $
��Left=�6)   $��
576,51,0,0q�6D   $	��Top=�6X   $	��177��6p   $��27��6|   $
��Bold"�6�   �� 5$   
   $��ALL SPACECRAFTS��6   $
��Left=�6*   $��
594,23,0,0q�6E   $	��Top=�6Y   $	��135��6q   $
��Bold"�6}   ���	#FF8B2727$��14(�6�   $��30��6�   5%      5&      