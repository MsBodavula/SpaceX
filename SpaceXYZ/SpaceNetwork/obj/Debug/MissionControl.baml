   M S B A M L   `   `   `  ����G  CSpaceNetwork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:SpaceNetworkSpaceNetwork  5   	   !    SpaceNetwork.MissionControl   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:SpaceNetwork     5   	     � Title$  MissionControl��5   	   $	��450��6    $	��800��6-   .��5	      � $��0,0,1,-1q�6   ���	#FFFFCC80 � ColumnDefinitions 5
   
   �� 5      $
��289* �6   �� 5      $	��50* �6   �� 5      $
��158* �6   �� 5      $
��294* �6   .+�5   
   �� $�� ��6   $
��Left=�6   $	��Top=�66   $��275,-13,0,0q�6N   �� 5   
   $��13(�6   $
��Bold"�6   ���	#FF8B2727$ ��SPACECRAFTS TO BE LAUNCHED��6G   $
��Left=�6l   $��	42,43,0,0q�6�   $	��Top=�6�   $��29��6�   $	��256��6�   $��2��6�   �� 5   
   $��13(�6   $
��Bold"�6   ���	#FF8B2727$��ACTIVE SPACECRAFTS��6G   $
��Left=�6d   $��
68,225,0,0q�6   $	��Top=�6�   $��29��6�   $	��184��6�   �� 5   
   -   5         ��Name$ listNonActiveSpacecrafts��5   %   $
��Bold"�6   $
��Left=�6G   $	��131��6b   $��	42,74,0,0q�6o   $	��Top=�6�   $	��205��6�   �� 5   
   -   5        $ listActiveSpacecrafts��5   %   $
��Bold"�6   $
��Left=�6E   $	��131��6`   $��
42,254,0,0q�6m   $	��Top=�6�   $	��205��6�   �� 5   
   $
��Left=�6   ���	#FFDF5A7C$	��158��6G   $��
11,231,0,0q�6T   $	��Top=�6h   $	��440��6�   $��3��6�   $��1��6�   .+�5      �� $
��Bold"�6   $��Select Spacecraft��6'   �� 5      -   5         ��Name$ comboNewSpacecrafts��5      $��	28,0,27,0q�64   $��41��6G   $
��Bold"�6S   �� 5      $
��Bold"�6   $��Launch��6'   �� 5      -   5         ��Name$ btnLaunchSpacecraft��5   (   $
��Bold"�6   {�. $��Launch Spacecraft��6V   $��42��6r   $��	27,0,29,0q�6~   $��16(�6�   5      �� 5   
   $��16(�6   $
��Bold"�6   ���	#FF8B2727$��Add New Spacecraft��6G   $��2��6d   $
��Left=�6t   $��
105,52,0,0q�6�   $	��Top=�6�   $��2��6�   $	��188��6�   �� 5   
   $��16(�6   $
��Bold"�6   ���	#FF8B2727$��Launch Spacecraft��6G   $��2��6c   $
��Left=�6s   $��105,186,0,0q�6�   $	��Top=�6�   $��2��6�   $	��152��6�   �� 5   
   -   5        $ btnBack��5   $   $
��Bold"�6   $
��Back��65   $
��Left=�6D   $��30,7,0,0q�6_   $	��Top=�6q   $��75��6�   6�   �� 5   
   -   5        $ 
btnRefresh��5   $   $
��Bold"�6   $��Refresh��68   $��3��6J   $
��Left=�6Z   $��200,0,0,388q�6u   $��Bottom=�6�   $��75��6�    J� RenderTransformOrigin$ 0.517,-0.472&�6�   6�   �� 5   
   -   5        $ btnAddNewSpacecraft��5      $��18(�6/   $
��Bold"�6=   $��3��6O   $$��ADD NEW SPACECRAFT AND PAYLOAD��6c   $��1��6�   $
��Left=�6�   $��	39,90,0,0q�6�   $	��Top=�6�   $	��384��6�   $��50��6�   ���	#FFBBE8F16  5       5!      