   M S B A M L   `   `   `  ����M  ICustomersAPIClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:CustomersAPIClientCustomersAPIClient  5   	   (    "CustomersAPIClient.ContactsNewEdit   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:CustomersAPIClient     5   	     � Title$  ContactsNewEdit��5   	   $	��150��6!   $��220.2��6.    � 
ResizeMode$ NoResize=�6<   .��5	      �  � RowDefinitions 5
   
   �� 5      $��1* �6   .+�5   
   �� $��New Contact��6   �� 5   
   $	��Top=�5      $��Stretch=�5      $��18��5      $��0,29,-0.4,0q�5   ^   ��5      �� *GreetingCombobox+ �� SelectedItem 5      �� *SelectedDataSource+ �� UpdateSourceTrigger$ PropertyChanged=�6^   �� 5   
   $��Stretch=�6   $��Center=�5      $��Arial)�5      $��14(�5      
���������5      ��  �� Path$ NewTxt�$ PropertyChanged=�.��6W   ��5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   -   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6-    �� 
TargetType" ��6    �� 	Resources 5         CueBannerBrush j� 
AlignmentX	 j� 
AlignmentY
 j� Stretch !� Visual��%�   &	        !� 5      $
 Left=�6=   $	 Center=�6O   $

 None=�6c    5      �� 5   "   $��Phone��6(   
�������68   5!      5"       �� Triggers 5#      P� 5$       P� Property � �� �� Empty P� Value# Z 62   . �5%      ��  �� Property � �� �� Value# [  6;   5&      P� 5'       � �� 62   Z� . �5(      ��  � ��# [  6;   5)      P� 5*       �@J�IsKeyboardFocused . . �5+      ��  � ��
 �����6;   5,      5.      .��5/      50      �� 51   
   $��Add contact��6   $��0��52      $��25��53      $��80��54      $��Bottom=�55      $��Right=�56      
����  �
���������59      �� *SaveCommand+5:      5;      