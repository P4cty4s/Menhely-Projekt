   M S B A M L   `   `   `  ����J  FMenhely Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Menhely_Projekt.ControlsMenhely_Projekt.Controls  5      ,    &Menhely_Projekt.Controls.KennelControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Menhely_Projekt.Controls     5      .��5	      � .+�5   
   )   #Menhely_Projekt.Controls.NavControl  -   5           Name$  NavBar��5   ?   $��50��6   $	��Top=�6'   6?   � 5   
   $��0,50,0,0q�6    � ColumnDefinitions 5      �� 5      $	��300 �6#   �� 5      $��* �6#   .+�5      �� -   5        $
��30px��5      $��100px��6#   $��0,10,0,0q�61   $	��Top=�6C   $��Frissítés��6[   $��20(�6�   �� 5      $��0��6   $
��Auto=�6+   
�������$��0,50,0,0q�6a   .��5      �� -   5         ��Name$ Kutyak_panel��5      $��10q�60    �� SelectionMode$ Multiple=�5       J� 	AllowDrop . $��16(�6�   5      � 5       $��1��6   $��20q�6#    � RowDefinitions 5!      �� 5"      $
��Auto �6$   �� 5#      $��* �6$   �� 5$      $
��Auto �6$   .+�5(      �� -   5         ��Name$ 
Udvarok_cb��5(      $	��300��6/   $��30��6;   $��0,0,0,10q�6G   $��20(�6�   �� 5+      $��1��6   $
��Auto=�6,   
�������.��5,      �� -   5         ��Name$ Kennel_panel��5,   !   $��10q�67   5-      �� 5/      $��3��6    �� Orientation$ 
Horizontal=�6*   $��Right=�6C   $��
0,10,10,10q�6_   .+�52      �� -   5        $	��300��52      $��30��6)   $��0,0,10,0q�65   
���  ��$��Kennelek kiürítése��6X   6u   �� 55      -   5        $��Kennel hozzáadása��55      $	��300��69   $��30��6E   6Q   �� 58      -   5        	 ��Name$	 Save_btn��58      $��Mentés��6/   $	��150��6@   $��30��6L   
���� �$��10,0,0,0q�6�   59      5:      5<      5=      5?      